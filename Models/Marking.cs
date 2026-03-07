// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Examer.Models;

[Table("marking")]
[Index(nameof(CommitId), Name = "marking_commit_id_idx")]
[Index(nameof(ReviewUserId), Name = "marking_review_user_id_idx")]
public class Marking : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("commit_id"), ForeignKey(nameof(Commit))]
    public Guid CommitId { get; set; }
    public Commit Commit { get; set; } = null!;

    [Column("review_user_id"), ForeignKey(nameof(ReviewUser))]
    public Guid ReviewUserId { get; set; }
    public User ReviewUser { get; set; } = null!;

    [Column("score")]
    public int Score { get; set; }

    [Column("comment")]
    public string Comment { get; set; } = string.Empty;
}
