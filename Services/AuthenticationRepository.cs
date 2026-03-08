// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.Net;
using System.Net.Mail;
using System.Security.Claims;

using Examer.Database;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper, IConfiguration configuration) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;

    public async Task<LoginResponseDto> LoginAsync(string studentNo, string password)
    {
        var user = await _context.Users
            .Where(x => x.Role != Role.Group)
            .Where(x => x.Enabled)
            .FirstOrDefaultAsync(x => x.StudentNumber == studentNo) ?? throw new NullReferenceException(nameof(studentNo));

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null!;

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Role, Enum.GetName(user.Role)!)
        };

        return new LoginResponseDto
        {
            UserId = user.Id,
            Token = _jwtHelper.GetJwtToken(claims),
            Role = user.Role,
            ExpirationTime = DateTime.Now.AddMinutes(30)
        };
    }

    public async Task RegisterAsync(User user)
    {
        if (_context.Users.Any(x => x.StudentNumber == user.StudentNumber))
            throw new NotUniqueException();

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        await _context.Users.AddAsync(user);
    }

    public void SendEmailAsync(User user)
    {
        var smtpConfig = new SmtpConfig();
        configuration.Bind("SmtpConfig", smtpConfig);

        var smtpClient = new SmtpClient(smtpConfig.Host, smtpConfig.Port)
        {
            Credentials = new NetworkCredential(smtpConfig.UserName, smtpConfig.Password),
            EnableSsl = smtpConfig.EnableSsl
        };

        var mailConfig = new MailConfig();
        configuration.Bind("MailConfig", mailConfig);

        var mail = new MailMessage()
        {
            From = new MailAddress(mailConfig.From),
            Subject = mailConfig.Subject,
            Body = string.Format(mailConfig.Body, user.EmailActivateToken)
        };

        mail.To.Add(user.Email);

        smtpClient.SendAsync(mail, null);
    }

    public async Task ActivateAsync(Guid emailActivateToken)
    {
        var user = await _context.Users
            .Where(x => x.EmailActivateToken == emailActivateToken)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(emailActivateToken));

        user.Enabled = true;
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
