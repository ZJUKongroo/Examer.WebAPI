using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Examer.Database;
using Examer.Helpers;
using Examer.Enums;
using Examer.Models;

namespace Examer.Services;

public class AuthenticationRepository(ExamerDbContext context, JwtHelper jwtHelper) : IAuthenticationRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly JwtHelper _jwtHelper = jwtHelper;

    // public async Task<string[]> LoginAsync(string studentNo, string password)
    // {
    //     ArgumentNullException.ThrowIfNullOrWhiteSpace(studentNo);
    //     ArgumentNullException.ThrowIfNullOrWhiteSpace(password);

    //     var user = await _context.Users!.FirstOrDefaultAsync(x => x.StudentNo == studentNo) ?? throw new NullReferenceException(nameof(studentNo));
    
    //     var encryption = SHA256.HashData(Encoding.UTF8.GetBytes(password + user.Salt));

    //     StringBuilder builder = new();
    //     for (int i = 0; i < encryption.Length; i++)
    //         builder.Append(encryption[i].ToString("X2"));
    //     var passwordEncryption = builder.ToString();

    //     if (user.Password == passwordEncryption)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new(ClaimTypes.Name, user.Id.ToString()),
    //             new(ClaimTypes.Role, Enum.GetName(typeof(Role), user.Role)!)
    //         };

    //         return [_jwtHelper.GetJwtToken(claims), user.Role.ToString()];
    //     }

    //     return null!;
    // }

    // public async Task RegisterStudentAsync(string studentNo, string name, string password)
    // {
    //     ArgumentException.ThrowIfNullOrWhiteSpace(studentNo);
    //     ArgumentException.ThrowIfNullOrWhiteSpace(password);

    //     var userExists = await _context.Users!.FirstOrDefaultAsync(x => x.StudentNo == studentNo);
    //     if (userExists != null)
    //     {
    //         throw new ArgumentException(nameof(studentNo));
    //     }

    //     var randomNumberGenerator = RandomNumberGenerator.Create();
    //     byte[] salt = new byte[128];
    //     randomNumberGenerator.GetBytes(salt);

    //     StringBuilder builder = new();
    //     for (int i = 0; i < 128; i++)
    //         builder.Append(salt[i].ToString("X2"));
    //     string saltString = builder.ToString();
    //     builder.Clear();

    //     var passwordBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + saltString));
    //     for (int i = 0; i < passwordBytes.Length; i++)
    //         builder.Append(passwordBytes[i].ToString("X2"));

    //     User user = new()
    //     {
    //         Id = Guid.NewGuid(),
    //         StudentNo = studentNo,
    //         Name = name,
    //         Role = Role.Student,
    //         Password = builder.ToString(),
    //         Salt = saltString,
    //         CreateTime = DateTime.Now,
    //         UpdateTime = DateTime.Now
    //     };
    //     await _context.Users!.AddAsync(user);

    //     // userInfo.Id = user.Id;
    //     // userInfo.CreateTime = DateTime.Now;
    //     // userInfo.UpdateTime = DateTime.Now;
        
    //     // await _context.UserInfos!.AddAsync(userInfo);
    // }

    // // public async Task RegisterAdministratorAsync(string studentNo, string password)
    // // {

    // // }

    // public async Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    // {
    //     if (userId == Guid.Empty)
    //         throw new ArgumentNullException(nameof(userId));
    //     ArgumentException.ThrowIfNullOrWhiteSpace(oldPassword);
    //     ArgumentException.ThrowIfNullOrWhiteSpace(newPassword);

    //     var user = await _context.Users!.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new ArgumentException(nameof(userId));

    //     var encryption = SHA256.HashData(Encoding.UTF8.GetBytes(oldPassword + user.Salt));
    //     StringBuilder builder = new();
    //     for (int i = 0; i < encryption.Length; i++)
    //         builder.Append(encryption[i].ToString("X2"));
    //     string oldPasswordEncryption = builder.ToString();

    //     if (oldPasswordEncryption != user.Password)
    //     {
    //         throw new ArgumentException(nameof(oldPassword));
    //     }
    //     builder.Clear();

    //     var randomNumberGenerator = RandomNumberGenerator.Create();
    //     byte[] salt = new byte[128];
    //     randomNumberGenerator.GetBytes(salt);

    //     for (int i = 0; i < salt.Length; i++)
    //         builder.Append(salt[i].ToString("X2"));
    //     string saltString = builder.ToString();
    //     builder.Clear();

    //     encryption = SHA256.HashData(Encoding.UTF8.GetBytes(newPassword + saltString));
    //     for (int i = 0; i < encryption.Length; i++)
    //         builder.Append(encryption[i].ToString("X2"));
    //     var newPasswordEncryption = builder.ToString();

    //     user.Password = newPasswordEncryption;
    //     user.Salt = saltString;
    // }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
