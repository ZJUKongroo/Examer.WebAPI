using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class MarkingRepository(ExamerDbContext context) : IMarkingRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<Marking>> GetMarkingsAsync(MarkingDtoParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var queryExpression = _context.Markings!
            .OrderBy(x => x.UpdateTime) as IQueryable<Marking>;

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Marking>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<Marking> GetMarkingAsync(Guid markingId)
    {
        if (markingId == Guid.Empty)
            throw new ArgumentNullException(nameof(markingId));

        return await _context.Markings!
            .Where(x => x.Id == markingId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(markingId));
    }

    public async Task AddMarkingAsync(Marking marking)
    {
        ArgumentNullException.ThrowIfNull(marking);

        await _context.Markings!.AddAsync(marking);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
