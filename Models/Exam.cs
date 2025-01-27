namespace Examer.Models;

public class Exam : ModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public ICollection<Problem> Problems { get; } = [];

    public List<User> Users { get; } = [];
    public List<UserExam> UserExams { get; } = [];

    public List<Exam> InheritedExam { get; } = [];
    public List<Exam> InheritingExam { get; } = [];
    public List<ExamInheritance> InheritedExamInheritance { get; } = [];
    public List<ExamInheritance> InheritingExamInheritance { get; } = [];
}
