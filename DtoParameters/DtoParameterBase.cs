namespace Examer.DtoParameters;

public class DtoParameterBase : IDtoParameterBase
{
    protected const int MaxPageSize = 10000;
    public int PageNumber { get; set; } = 1;
    protected int _pageSize = MaxPageSize;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
