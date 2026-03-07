// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Examer.Models;

[Table("user_exam")]
[Index(nameof(UserId), Name = "user_exam_user_id_idx")]
[Index(nameof(ExamId), Name = "user_exam_exam_id_idx")]
public class UserExam : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("user_id"), ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Column("exam_id"), ForeignKey(nameof(ExamId))]
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    // Commit Table
    public List<Commit> Commits { get; } = [];
}
