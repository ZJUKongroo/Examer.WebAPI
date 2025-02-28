namespace Examer.Dtos;

public class CommitWithMarkingsDto : CommitDto
{
    public List<MarkingDto> Markings { get; set; } = [];
}
