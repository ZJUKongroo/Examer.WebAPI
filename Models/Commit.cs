namespace Examer.Models;

public class Commit : ModelBase
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public DateTime CommitTime { get; set; }
    public List<Marking> Markings { get; } = [];

    // The foreign key to ExamUser table
    public Guid UserExamId { get; set; }
    public UserExam UserExam { get; set; } = null!;

    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;

    public List<ExamerFile> Files { get; } = [];
}
