using Examer.Enums;

namespace Examer.DtoParameters;

public class UserDtoParameter : DtoParameterBase
{
    // Filtering fields
    public string? Name { get; set; }
    public string? StudentNo { get; set; }
    // public Role Role { get; set; }
    // public Guid GroupId { get; set; }
    public Gender Gender { get; set; }
    public EthnicGroup EthnicGroup { get; set; }
    // public DateOnly DateOfBirth { get; set; }
    public string? PhoneNo { get; set; }
    public string? College { get; set; }
    public string? Major { get; set; }
    public string? Class { get; set; }
    public string? Campus { get; set; }
    public string? Dormitory { get; set; }
    public PoliticalStatus PoliticalStatus { get; set; }
    public string? HomeAddress { get; set; }
}
