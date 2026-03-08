// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Examer.Enums;

namespace Examer.Models;

[Table("user_detail")]
public class UserDetail : ModelBase
{
    [Column("user_id"), Key, ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("gender"), EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    [Column("ethnic_group"), EnumDataType(typeof(EthnicGroup))]
    public EthnicGroup EthnicGroup { get; set; }

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Column("serior_high")]
    public string SeniorHigh { get; set; } = string.Empty;

    [Column("college")]
    public string College { get; set; } = string.Empty;

    [Column("major")]
    public string Major { get; set; } = string.Empty;

    [Column("class")]
    public string Class { get; set; } = string.Empty;

    [Column("dormitory")]
    public string Dormitory { get; set; } = string.Empty;

    [Column("political_status"), EnumDataType(typeof(PoliticalStatus))]
    public PoliticalStatus PoliticalStatus { get; set; }

    [Column("home_address")]
    public string HomeAddress { get; set; } = string.Empty;

    [Column("english_level")]
    public string EnglishLevel { get; set; } = string.Empty;

    [Column("gpa_of_all_courses")]
    public float GpaOfAllCourses { get; set; }

    [Column("gpa_of_major_courses")]
    public float GpaOfMajorCourses { get; set; }

    [Column("rank")]
    public int Rank { get; set; }

    [Column("college_number")]
    public int CollegeNumber { get; set; }
}
