using Examer.Models;

namespace Examer.Dtos;

public class GroupDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<User> Users { get; set; } = [];
}
