namespace Examer.Dtos;

public class AddCommitDto
{
    public string? Description { get; set; }
    public Guid ExamId { get; set; }
    public Guid ProblemId { get; set; }
    public Guid UserId { get; set; }
}
