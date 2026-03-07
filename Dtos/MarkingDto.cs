// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class MarkingDto
{
    public Guid Id { get; set; }
    public Guid CommitId { get; set; }
    public Guid ReviewUserId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; } = string.Empty;
}

public class AddMarkingDto
{
    [Required]
    public Guid CommitId { get; set; }

    [Required]
    public Guid ReviewUserId { get; set; }

    [Required]
    public int Score { get; set; }

    public string Comment { get; set; } = string.Empty;
}

public class UpdateMarkingDto
{
    public int? Score { get; set; }

    public string? Comment { get; set; }
}
