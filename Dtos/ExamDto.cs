// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class ExamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ExamType ExamType { get; set; }
    public bool IsPublic { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<ProblemDto> Problems { get; set; } = [];
}

public class ExamWithUsersDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ExamType ExamType { get; set; }
    public bool IsPublic { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<Guid> Users { get; set; } = [];
}

public class AddExamDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ExamType ExamType { get; set; }

    [Required]
    public bool IsPublic { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}

public class UpdateExamDto
{
    public string? Name { get; set; }

    public bool? IsPublic { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }
}
