namespace Examer.Models;

public class Commit : IModelBase
{
    public Guid Id { get; set; }
    public DateTime CommitTime { get; set; }

    // The UserId and ProblemId fields are foreign keys
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;

    // The implementation of IModelBase interface
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}
