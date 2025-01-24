using Examer.Enums;

namespace Examer.Models;

public class User : ModelBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? StudentNo { get; set; } // The StudentNo is null for Group role
    public Role Role { get; set; }
    public string? Description { get; set; }

    // User table and Exam table have many-to-many relationship
    public List<Exam> Exams { get; } = [];
    public List<UserExam> UserExams { get; } = [];

    // Self-referencing many-to-many (Group options)
    public List<User> GroupUsers { get; } = [];
    public List<User> UsersOfGroup { get; } = [];
    public List<Group> Groups { get; } = []; // For GroupUser navigation property
    public List<Group> Users { get; } = []; // For User navigation property

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
}
