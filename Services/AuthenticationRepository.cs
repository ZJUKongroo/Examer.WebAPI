// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.Net;
using System.Net.Mail;
using System.Security.Claims;

using Examer.Database;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;

    public async Task<LoginResponseDto> LoginAsync(string studentNo, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentNo);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var user = await _context.Users!
            .Where(x => x.Role != Role.Group)
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

    public async Task RegisterAsync()
    {
        var smtpClient = new SmtpClient("smtp.zju.edu.cn", 587)
        {
            Credentials = new NetworkCredential("username", "password"),
            EnableSsl = true
        };

        var mail = new MailMessage();
        mail.From = new MailAddress("sender@zju.edu.cn");
        mail.To.Add("receive@zju.edu.cn");
        mail.Subject = "Test Email";
        mail.Body = "This is a test mail.";

        smtpClient.SendAsync(mail, null);
    }

    public async Task ActivateAsync()
    {

    }
}
