// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Examer.Enums;

using Microsoft.EntityFrameworkCore;

namespace Examer.Models;

[Table("problem")]
[Index(nameof(ExamId), Name = "problem_exam_id_idx")]
public class Problem : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("score")]
    public int Score { get; set; }

    [Column("problem_type"), EnumDataType(typeof(ProblemType))]
    public ProblemType ProblemType { get; set; }

    [Column("exam_id"), ForeignKey(nameof(Exam))]
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    // Commit Table
    public List<Commit> Commits { get; } = [];

    // ExamerFile Table
    public List<ExamerFile> Files { get; } = [];
}
