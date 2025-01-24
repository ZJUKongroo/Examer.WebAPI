using Examer.Enums;

namespace Examer.Dtos;

public class LoginDto
{
    public string? Token { get; set; }
    public Role Role { get; set; }
    public DateTime ExpirationTime { get; set; }
}
