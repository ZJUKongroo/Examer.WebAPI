// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public Role Role { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public Gender Gender { get; set; }
    public EthnicGroup EthnicGroup { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string College { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Campus { get; set; } = string.Empty;
    public string Dormitory { get; set; } = string.Empty;
    public PoliticalStatus PoliticalStatus { get; set; }
    public string HomeAddress { get; set; } = string.Empty;
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

    [Required]
    public Role Role { get; set; }

    public string Description { get; set; } = string.Empty;

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public EthnicGroup EthnicGroup { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required, Length(11, 11)]
    [RegularExpression(@"^\d{11}$")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string College { get; set; } = string.Empty;

    [Required]
    public string Major { get; set; } = string.Empty;

    [Required]
    public string Class { get; set; } = string.Empty;

    [Required]
    public string Campus { get; set; } = string.Empty;

    [Required]
    public string Dormitory { get; set; } = string.Empty;

    [Required]
    public PoliticalStatus PoliticalStatus { get; set; }

    [Required]
    public string HomeAddress { get; set; } = string.Empty;
}

public class UpdateUserDto
{
    public Gender? Gender { get; set; }
    public EthnicGroup? EthnicGroup { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    [Length(11, 11)]
    [RegularExpression(@"^\d{11}$")]
    public string? PhoneNumber { get; set; }
    public string? College { get; set; }
    public string? Major { get; set; }
    public string? Class { get; set; }
    public string? Campus { get; set; }
    public string? Dormitory { get; set; }
    public PoliticalStatus? PoliticalStatus { get; set; }
    public string? HomeAddress { get; set; }
}
