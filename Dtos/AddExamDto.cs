using Examer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class AddExamDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public ExamType ExamType { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}
