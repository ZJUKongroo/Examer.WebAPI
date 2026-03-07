// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class CommitDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CommitTime { get; set; }
    public UserDto User { get; set; } = null!;
    public ExamDto Exam { get; set; } = null!;
    public ProblemDto Problem { get; set; } = null!;
    public List<ExamerFileDto> Files { get; set; } = [];
}

public class CommitWithMarkingsDto : CommitDto
{
    public List<MarkingDto> Markings { get; set; } = [];
}

public class AddCommitDto
{
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid ExamId { get; set; }

    [Required]
    public Guid ProblemId { get; set; }

    [Required]
    public Guid UserId { get; set; }
}

public class UpdateCommitDto
{
    public string? Description { get; set; }
}
