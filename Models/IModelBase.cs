namespace Examer.Models;

// The required fields in all tables
public interface IModelBase
{
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; }
}
