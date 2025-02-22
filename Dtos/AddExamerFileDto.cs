using Examer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class AddExamerFileDto
{
    [Required]
    public string? FileName { get; set; }
    public Guid ParentId { get; set; }
    public FileType FileType { get; set; }
}
