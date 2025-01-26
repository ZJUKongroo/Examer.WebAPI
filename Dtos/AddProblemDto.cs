using System.ComponentModel.DataAnnotations;
using Examer.Enums;

namespace Examer.Dtos;

public class AddProblemDto
{
    public Guid ExamId { get; set; }
    public int Index { get; set; }
    [Required]
    public string? Description { get; set; }
    public ProblemType ProblemType { get; set; }
}
