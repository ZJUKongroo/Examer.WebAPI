// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Examer.Enums;

namespace Examer.Models;

[Table("exam")]
public class Exam : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("exam_type"), EnumDataType(typeof(ExamType))]
    public ExamType ExamType { get; set; }

    [Column("is_public")]
    public bool IsPublic { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("end_time")]
    public DateTime EndTime { get; set; }

    // Problem Table
    public List<Problem> Problems { get; } = [];

    // User Table
    public List<User> Users { get; } = [];

    // UserExam Table
    public List<UserExam> UserExams { get; } = [];
}
