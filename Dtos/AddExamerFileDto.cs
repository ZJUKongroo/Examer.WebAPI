using Examer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class AddExamerFileDto
{
    public Guid ParentId { get; set; }
    public FileType FileType { get; set; }
}
