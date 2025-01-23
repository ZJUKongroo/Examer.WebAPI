namespace Examer.Models;

public class ExamUser : IModelBase
{
    // The User table and Exam table have many-to-many relationship
    public Guid ExamsId { get; set; }
    public Guid UsersId { get; set; }

    // The implementation of IModelBase interface
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}
