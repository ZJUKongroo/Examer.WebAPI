using Examer.Enums;

namespace Examer.Dtos;

public class ExamerFileDto
{
    public Guid Id { get; set; }
    public string? FileName { get; set; }
    public FileType FileType { get; set; }
}
