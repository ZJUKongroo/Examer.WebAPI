// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Examer.Models;

[Table("commit")]
[Index(nameof(UserExamId), Name = "commit_user_exam_id_idx")]
[Index(nameof(ProblemId), Name = "commit_problem_id_idx")]
public class Commit : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("commit_time")]
    public DateTime CommitTime { get; set; }

    // Marking Table
    public List<Marking> Markings { get; } = [];

    [Column("user_exam_id"), ForeignKey(nameof(UserExam))]
    public Guid UserExamId { get; set; }
    public UserExam UserExam { get; set; } = null!;

    [Column("problem_id"), ForeignKey(nameof(Problem))]
    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;

    // ExamerFile Table
    public List<ExamerFile> Files { get; } = [];
}
