namespace Examer.Dtos;

public class ExamDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public List<ProblemDto> Problems { get; set; } = [];
}
