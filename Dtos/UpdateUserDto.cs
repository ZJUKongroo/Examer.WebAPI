using System.ComponentModel.DataAnnotations;
using Examer.Enums;

namespace Examer.Dtos;

public class UpdateUserDto
{
    public Gender Gender { get; set; }
    public EthnicGroup EthnicGroup { get; set; }
    public DateOnly DateOfBirth { get; set; }
    [Required, Length(11, 11)]
    public string? PhoneNo { get; set; }
    [Required]
    public string? College { get; set; }
    [Required]
    public string? Major { get; set; }
    [Required]
    public string? Class { get; set; }
    [Required]
    public string? Campus { get; set; }
    [Required]
    public string? Dormitory { get; set; }
    public PoliticalStatus PoliticalStatus { get; set; }
    [Required]
    public string? HomeAddress { get; set; }
}
