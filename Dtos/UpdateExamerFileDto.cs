using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class UpdateExamerFileDto
{
    [Required]
    public string? FileName { get; set; }
}
