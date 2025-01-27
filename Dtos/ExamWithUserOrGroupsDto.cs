namespace Examer.Dtos;

public class ExamWithUserOrGroupsDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<Guid> UserOrGroupIds { get; set; } = [];
}
