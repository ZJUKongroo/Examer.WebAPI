// Copyright (c) ZJUKongroo. All Rights Reserved.

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

    public async Task<LoginDto> LoginAsync(string studentNo, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(studentNo);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        var user = await _context.Users!
            .Where(x => x.Role != Role.Group)
            .FirstOrDefaultAsync(x => x.StudentNumber == studentNo) ?? throw new NullReferenceException(nameof(studentNo));

        var passwordEncryption = BCrypt.Net.BCrypt.HashPassword(password);

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
