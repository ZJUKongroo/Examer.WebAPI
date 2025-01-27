using Examer.Enums;

namespace Examer.Dtos;

public class UserWithExamIdsDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? StudentNo { get; set; }
    public Role Role { get; set; }
    public string? Description { get; set; }
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
    public List<Guid> ExamIds { get; } = [];
}
