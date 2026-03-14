// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Examer.Enums;

using Microsoft.EntityFrameworkCore;

namespace Examer.Models;

[Table("user")]
[Index(nameof(StudentNumber), Name = "user_student_number_uidx", IsUnique = true)]
[Index(nameof(ActivateAccountToken), Name = "user_activate_account_token_uidx", IsUnique = true)]
[Index(nameof(ResetPasswordToken), Name = "user_reset_password_token_uidx", IsUnique = true)]
public class User : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    // The StudentNumber is empty for Group role
    [Column("student_number")]
    public string StudentNumber { get; set; } = string.Empty;

    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("activate_account_token")]
    public Guid ActivateAccountToken { get; set; }

    [Column("reset_password_token")]
    public Guid? ResetPasswordToken { get; set; }

    [Column("role"), EnumDataType(typeof(Role))]
    public Role Role { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("enabled")]
    public bool Enabled { get; set; }

    // Exam Table
    public List<Exam> Exams { get; } = [];

    // UserExam Table
    public List<UserExam> UserExams { get; } = [];

    // Self-referencing many-to-many (Group options)
    public List<User> GroupUsers { get; } = [];
    public List<User> UsersOfGroup { get; } = [];
    public List<Group> Groups { get; } = []; // For GroupUser navigation property
    public List<Group> Users { get; } = []; // For User navigation property

    // Marking Table
    public List<Marking> Markings { get; } = [];
}
