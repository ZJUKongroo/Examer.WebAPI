namespace Examer.Dtos;

public class AddMarkingDto
{
    public Guid CommitId { get; set; }
    public Guid ReviewUserId { get; set; }
    public int Score { get; set; }
}
