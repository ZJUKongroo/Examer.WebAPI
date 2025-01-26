using Examer.Models;

namespace Examer.Dtos;

public class GroupDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<UserDto> Users { get; set; } = [];
}
