using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class UpdateExamDto
{
    [Required]
    public string? Name { get; set; }
    // [Required]
    // public ExamType ExamType { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
