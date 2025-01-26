using Examer.Enums;

namespace Examer.Models;

public class Problem : ModelBase
{
    public Guid Id { get; set; }
    public int Index { get; set; }
    public string? Description { get; set; }
    public string? StorageLocation { get; set; }
    public ProblemType ProblemType { get; set; }
    
    // The Problem table and Exam Table have one-to-many relationship
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public List<Commit> Commits { get; } = [];
}
