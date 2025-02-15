using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Services;

public interface IMarkingRepository
{
    Task<PagedList<Marking>> GetMarkingsAsync(MarkingDtoParameter parameter);
    Task<Marking> GetMarkingAsync(Guid markingId);
    Task AddMarkingAsync(Marking marking);
    Task<bool> SaveAsync();
}
