// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}

public class UserWithExamIdsDto : UserDto
{
    public List<Guid> ExamIds { get; } = [];
}

public class AddUserDto
{
    [Required, Length(2, 4)]
    [RegularExpression(@"^[\u4e00-\u9fa5]{2,4}$")]
    public string Name { get; set; } = string.Empty;

    [Required, Length(10, 10)]
    [RegularExpression(@"^\d{10}$")]
    public string StudentNumber { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public Role Role { get; set; }

    public string Description { get; set; } = string.Empty;
}
