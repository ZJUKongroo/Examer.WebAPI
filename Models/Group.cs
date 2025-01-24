namespace Examer.Models;

public class Group : ModelBase
{
    public Guid GroupId { get; set; }
    public Guid UserOfGroupId { get; set; }
    public User GroupUser { get; set; } = null!;
    public User User { get; set; } = null!;
}
