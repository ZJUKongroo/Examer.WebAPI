// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.Security.Claims;

using Examer.Database;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.EntityFrameworkCore;

using MimeKit;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper, IConfiguration configuration) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;

    private LoginResponseDto GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        return new LoginResponseDto
        {
            UserId = user.Id,
            Token = _jwtHelper.GetJwtToken(claims),
            Role = user.Role,
            ExpirationTime = DateTime.Now.AddMinutes(30)
        };
    }

    public async Task<LoginResponseDto> LoginAsync(string studentNumber, string password)
    {
        var user = await _context.Users
            .Where(x => x.Role != Role.Group)
            .Where(x => x.Enabled)
            .FirstOrDefaultAsync(x => x.StudentNumber == studentNumber) ?? throw new NotFoundException(nameof(studentNumber));

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null!;

        return GenerateToken(user);
    }

    public async Task RegisterAsync(User user)
    {
        if (_context.Users.Any(x => x.StudentNumber == user.StudentNumber))
            throw new NotUniqueException();

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await _context.Users.AddAsync(user);
    }

    public async Task SendEmailAsync(User user, EmailType emailType)
    {
        var smtpConfig = new SmtpConfig();
        configuration.Bind("SmtpConfig", smtpConfig);

        var mailConfig = new MailConfig();
        configuration.Bind("MailConfig", mailConfig);

        string domain = configuration["Domain"]!;

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("ACEE Exam System", mailConfig.From));
        message.To.Add(MailboxAddress.Parse(user.Email));

        var builder = new BodyBuilder();

        switch (emailType)
        {
            case EmailType.ActivateAccountEmail:
                message.Subject = mailConfig.ActivateAccountSubject;
                builder.TextBody = string.Format(mailConfig.ActivateAccountBody, domain, user.ActivateAccountToken);
                builder.HtmlBody = string.Format(mailConfig.ActivateAccountBody, domain, user.ActivateAccountToken);
                break;
            case EmailType.ResetPasswordEmail:
                message.Subject = mailConfig.ResetPasswordSubject;
                builder.TextBody = string.Format(mailConfig.ResetPasswordBody, domain, user.ResetPasswordToken);
                builder.HtmlBody = string.Format(mailConfig.ResetPasswordBody, domain, user.ResetPasswordToken);
                break;
            default:
                throw new NotSupportedException(nameof(emailType));
        }

        message.Body = builder.ToMessageBody();

        message.Headers.Add("X-Mailer", "Microsoft Outlook 16.0");

        using var client = new SmtpClient();
        var secureSocket = smtpConfig.EnableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable;

        await client.ConnectAsync(smtpConfig.Host, smtpConfig.Port, secureSocket);
        await client.AuthenticateAsync(smtpConfig.UserName, smtpConfig.Password);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    public async Task<LoginResponseDto> ActivateAsync(Guid emailActivateToken)
    {
        var user = await _context.Users
            .Where(x => x.ActivateAccountToken == emailActivateToken)
            .Where(x => !x.Enabled)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(emailActivateToken));

        user.Enabled = true;

        return GenerateToken(user);
    }

    public async Task<User> GetUserByStudentNumberAsync(string studentNumber)
    {
        return await _context.Users
            .Where(x => x.StudentNumber == studentNumber)
            .Where(x => x.Enabled)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(studentNumber));
    }

    public async Task<LoginResponseDto> UpdatePasswordAsync(Guid passwordResetToken, string password)
    {
        if (passwordResetToken == Guid.Empty)
            throw new EmptyGuidException(nameof(passwordResetToken));

        var user = await _context.Users
            .Where(x => x.ResetPasswordToken == passwordResetToken)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(passwordResetToken));

        user.Password = BCrypt.Net.BCrypt.HashPassword(password);
        user.ResetPasswordToken = null;

        return GenerateToken(user);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
