// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class UserDetailDto
{
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
    public string EnglishLevel { get; set; } = string.Empty;
    public float GpaOfAllCourses { get; set; }
    public float GpaOfMajorCourses { get; set; }
    public int Rank { get; set; }
    public int CollegeNumber { get; set; }
}

public class AddUserDetailDto
{
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

public class UpdateUserDetailDto
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
