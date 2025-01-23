using Examer.Enums;

namespace Examer.Dtos;

public class UpdateUserDto
{
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
