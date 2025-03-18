using Examer.Database;
using Examer.Helpers;
using Examer.Enums;
using Examer.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;

    public async Task<LoginDto> LoginAsync(string studentNo, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentNo);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
    
        var user = await _context.Users!
            .Where(x => x.Role != Role.Group)
            .FirstOrDefaultAsync(x => x.StudentNo == studentNo) ?? throw new NullReferenceException(nameof(studentNo));

        var encryption = SHA256.HashData(Encoding.UTF8.GetBytes(password + user.Salt));

        StringBuilder builder = new();
        for (int i = 0; i < encryption.Length; i++)
            builder.Append(encryption[i].ToString("X2"));
        var passwordEncryption = builder.ToString();

        if (user.Password != passwordEncryption)
            return null!;

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Role, Enum.GetName(user.Role)!)
        };

        return new LoginDto
        {
            UserId = user.Id,
            Token = _jwtHelper.GetJwtToken(claims),
            Role = user.Role,
            ExpirationTime = DateTime.Now.AddMinutes(30)
        };
    }
}
