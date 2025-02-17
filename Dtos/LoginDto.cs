using Examer.Enums;

namespace Examer.Dtos;

public class LoginDto
{
    public Guid UserId { get; set; }
    public string? Token { get; set; }
    public Role Role { get; set; }
    public DateTime ExpirationTime { get; set; }
}
