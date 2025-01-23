namespace Examer.Models;

public class Exam : IModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public ICollection<Problem> Problems { get; } = new List<Problem>();

    public List<User> Users { get; } = [];

    // The implementation of IModelBase interface
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}
