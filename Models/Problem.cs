namespace Examer.Models;

public class Problem : IModelBase
{
    public Guid Id { get; set; }

    // The Problem table and Exam Table have one-to-many relationship
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public ICollection<Commit> Commits { get; } = new List<Commit>();

    // The implementation of IModelBase interface
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}