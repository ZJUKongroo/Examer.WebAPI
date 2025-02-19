namespace Examer.Models;

public class UserExam : ModelBase
{
    public Guid Id { get; set; }

    // The User table and Exam table have many-to-many relationship
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public User User { get; set; } = null!;
    public Exam Exam { get; set; } = null!;

    // Commit table navigation property
    public ICollection<Commit> Commits { get; } = [];
}
