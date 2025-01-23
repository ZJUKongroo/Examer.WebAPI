using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class AddExamDto
{
    [Required]
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
