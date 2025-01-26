using System.ComponentModel.DataAnnotations;
using Examer.Enums;

namespace Examer.Dtos;

public class AddProblemDto
{
    public Guid ExamId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public ProblemType ProblemType { get; set; }
}
