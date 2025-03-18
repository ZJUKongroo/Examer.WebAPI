namespace Examer.Models;

public class Marking : ModelBase
{
    public Guid Id { get; set; }
    public Guid CommitId { get; set; }
    public Commit Commit { get; set; } = null!;
    public Guid ReviewUserId { get; set; }
    public User ReviewUser { get; set; } = null!;
    public int Score { get; set; }
    public string? Comment { get; set; }
}
