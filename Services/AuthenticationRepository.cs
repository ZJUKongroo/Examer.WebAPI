// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.Security.Claims;

using Examer.Database;

using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;
using MimeKit.Utils;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper, IConfiguration configuration, ILogger<AuthenticationRepository> logger) : IAuthenticationRepository
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

    public async Task<LoginResponseDto> LoginAsync(string studentNo, string password)
    {
        var user = await _context.Users
            .Where(x => x.Role != Role.Group)
            .Where(x => x.Enabled)
            .FirstOrDefaultAsync(x => x.StudentNumber == studentNo) ?? throw new NotFoundException(nameof(studentNo));

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

    public async Task SendEmailAsync(User user)
    {
        var smtpConfig = new SmtpConfig();
        configuration.Bind("SmtpConfig", smtpConfig);

            var mailConfig = new MailConfig();
            configuration.Bind("MailConfig", mailConfig);

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("ACEE Exam System", mailConfig.From));
        message.To.Add(MailboxAddress.Parse(user.Email));
        message.Subject = mailConfig.Subject;

        var builder = new BodyBuilder
        {
            TextBody = string.Format(mailConfig.Body, user.EmailActivateToken),
            HtmlBody = string.Format(mailConfig.Body, user.EmailActivateToken)
        };
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
            .Where(x => x.EmailActivateToken == emailActivateToken)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(emailActivateToken));

        user.Enabled = true;

        return GenerateToken(user);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
