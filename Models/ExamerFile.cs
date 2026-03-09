// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Examer.Enums;

using Microsoft.EntityFrameworkCore;

namespace Examer.Models;

[Table("file")]
[Index(nameof(ProblemId), Name = "file_problem_id_idx")]
[Index(nameof(CommitId), Name = "file_commit_id_idx")]
public class ExamerFile : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("file_name")]
    public string FileName { get; set; } = string.Empty;

    [Column("file_size")]
    public long FileSize { get; set; }

    [Column("problem_id"), ForeignKey(nameof(Problem))]
    public Guid? ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;

    [Column("commit_id"), ForeignKey(nameof(Commit))]
    public Guid? CommitId { get; set; }
    public Commit Commit { get; set; } = null!;

    [Column("file_type"), EnumDataType(typeof(FileType))]
    public FileType FileType { get; set; }
}
