using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class UpdateExamDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}
