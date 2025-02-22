using Examer.Enums;

namespace Examer.Models;

public class ExamerFile : ModelBase
{
    public Guid Id { get; set; }
    public string? FileName { get; set; }
    public Guid? ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;
    public Guid? CommitId { get; set; }
    public Commit Commit { get; set; } = null!;
    public FileType FileType { get; set; }
}
