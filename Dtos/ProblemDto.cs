using Examer.Enums;

namespace Examer.Dtos;

public class ProblemDto
{
    public Guid Id { get; set; }
    public int Index { get; set; }
    public string? Description { get; set; }
    public ProblemType ProblemType { get; set; }
}
