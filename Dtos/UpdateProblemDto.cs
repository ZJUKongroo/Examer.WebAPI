// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class UpdateProblemDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public int Score { get; set; }
    public ProblemType ProblemType { get; set; }
}
