using Examer.Enums;

namespace Examer.Models;

public class User : IModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    // The StudentNo is null for Group role
    public string? StudentNo { get; set; }

    public Role Role { get; set; }

    public List<Exam> Exams { get; } = [];

    // Self-referencing one-to-many (Group options)
    public IList<User> Groups { get; } = [];
    public IList<User> UsersOfGroup { get; } = [];

    // Commit table navigation property
    public ICollection<Commit> Commits { get; } = [];

    // Detailed User information
    public Gender Gender { get; set; }
    public EthnicGroup EthnicGroup { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhoneNo { get; set; }
    public string? College { get; set; }
    public string? Major { get; set; }
    public string? Class { get; set; }
    public string? Campus { get; set; }
    public string? Dormitory { get; set; }
    public PoliticalStatus PoliticalStatus { get; set; }
    public string? HomeAddress { get; set; }

    // The implementation of IModelBase interface
    public DateTime CreateTime { get; set; }
    public DateTime ModifyTime { get; set; }
    public DateTime DeleteTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}
