// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class ProblemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Score { get; set; }
    public ProblemType ProblemType { get; set; }
    public List<ExamerFileDto> Files { get; set; } = [];
}

public class AddProblemDto
{
    [Required]
    public Guid ExamId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public int Score { get; set; }

    [Required]
    public ProblemType ProblemType { get; set; }
}

public class UpdateProblemDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Score { get; set; }
    public ProblemType? ProblemType { get; set; }
}
