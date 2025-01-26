using Examer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class UpdateProblemDto
{
    public int Index { get; set; }
    [Required]
    public string? Description { get; set; }
    public ProblemType ProblemType { get; set; }
}
