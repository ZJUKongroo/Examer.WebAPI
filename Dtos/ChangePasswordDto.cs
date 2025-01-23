using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class ChangePasswordDto
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string? OldPassword { get; set; }
    [Required]
    public string? NewPassword { get; set; }
}