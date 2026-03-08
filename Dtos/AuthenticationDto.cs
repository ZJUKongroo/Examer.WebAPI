// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class LoginResponseDto
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public Role Role { get; set; }
    public DateTime ExpirationTime { get; set; }
}

public class LoginDto
{
    [Required, Length(10, 10)]
    [RegularExpression(@"^\d{10}$")]
    public string StudentNumber { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*? ]).{8,}$")]
    public string Password { get; set; } = string.Empty;
}

public class RegisterDto
{
    [Required, Length(10, 10)]
    [RegularExpression(@"^\d{10}$")]
    public string StudentNumber { get; set; } = string.Empty;

    [Required, Length(2, 4)]
    [RegularExpression(@"^[\u4e00-\u9fa5]{2,4}$")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*? ]).{8,}$")]
    public string Password { get; set; } = string.Empty;
}
