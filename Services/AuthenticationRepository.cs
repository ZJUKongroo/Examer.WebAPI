// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.Security.Claims;

using Examer.Database;

using MailKit.Net.Smtp;
using MailKit.Security;

using MimeKit;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper, IConfiguration configuration, ILogger<AuthenticationRepository> logger) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;
    private readonly ILogger<AuthenticationRepository> _logger = logger;

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

    public async Task<bool> SendEmailAsync(User user)
    {
        try
        {
            var smtpConfig = new SmtpConfig();
            configuration.Bind("SmtpConfig", smtpConfig);

            var mailConfig = new MailConfig();
            configuration.Bind("MailConfig", mailConfig);

            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(mailConfig.From));
            message.To.Add(MailboxAddress.Parse(user.Email));
            message.Subject = mailConfig.Subject;
            message.Body = new TextPart("html")
            {
                Text = string.Format(mailConfig.Body, user.EmailActivateToken)
            };

            using var client = new SmtpClient();
            var secureSocket = smtpConfig.EnableSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTlsWhenAvailable;

            await client.ConnectAsync(smtpConfig.Host, smtpConfig.Port, secureSocket);
            await client.AuthenticateAsync(smtpConfig.UserName, smtpConfig.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return true;
        }
        catch (AuthenticationException ex)
        {
            _logger.LogError(ex, "SMTP 认证失败: 用户名或密码错误");
            return false;
        }
        catch (SmtpCommandException ex)
        {
            _logger.LogError(ex, "SMTP 命令错误: StatusCode={StatusCode}, ErrorCode={ErrorCode}", ex.StatusCode, ex.ErrorCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "发送邮件到 {Email} 时发生未知错误", user.Email);
            return false;
        }
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
