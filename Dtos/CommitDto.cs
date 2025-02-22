namespace Examer.Dtos;

public class CommitDto
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    // public int Score { get; set; }
    public DateTime CommitTime { get; set; }
    public UserDto User { get; set; } = null!;
    public ExamDto Exam { get; set; } = null!;
    public ProblemDto Problem { get; set; } = null!;
    public List<MarkingDto> Markings { get; set; } = [];
    public List<ExamerFileDto> Files { get; set; } = [];
}
