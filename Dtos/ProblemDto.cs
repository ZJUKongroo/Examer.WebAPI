using Examer.Enums;
using Examer.Models;

namespace Examer.Dtos;

public class ProblemDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Score { get; set; }
    public ProblemType ProblemType { get; set; }
    public List<ExamerFileDto> Files { get; set; } = [];
}
