namespace Examer.Models;

public class ModelBase : IModelBase
{
    // The implementation of IModelBase interface
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}
