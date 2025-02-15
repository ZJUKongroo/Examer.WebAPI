namespace Examer.DtoParameters;

public class MarkingDtoParameter : DtoParameterBase
{
    public Guid CommitId { get; set; }
    public Guid ReviewUserId { get; set; }
    public int Score { get; set; }
}
