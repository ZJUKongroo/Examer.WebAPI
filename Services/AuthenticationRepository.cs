using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Examer.Database;
using Examer.Helpers;
using Examer.Enums;
using Examer.Dtos;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;

    public async Task<LoginDto> LoginAsync(string studentNo)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentNo);
    
        var user = await _context.Users!
            .Where(x => x.Role != Role.Group)
            .FirstOrDefaultAsync(x => x.StudentNo == studentNo) ?? throw new NullReferenceException(nameof(studentNo));

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
