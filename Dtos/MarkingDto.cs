namespace Examer.Dtos;

public class MarkingDto
{
    public Guid Id { get; set; }
    public Guid CommitId { get; set; }
    public Guid ReviewUserId { get; set; }
    public int Score { get; set; }
}
