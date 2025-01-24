namespace Examer.Models;

public class Commit : ModelBase
{
    public Guid Id { get; set; }
    public DateTime CommitTime { get; set; }

    // The foreign key to ExamUser table
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public UserExam ExamUser { get; set; } = null!;

    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;
}
