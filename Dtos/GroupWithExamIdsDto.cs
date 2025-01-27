namespace Examer.Dtos;

public class GroupWithExamIdsDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<UserDto> Users { get; } = [];
    public List<Guid> ExamIds { get; } = [];
}
