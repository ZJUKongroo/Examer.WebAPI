// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class MarkingRepository(ExamerDbContext context) : IMarkingRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<Marking>> GetMarkingsAsync(MarkingDtoParameter parameter)
    {
        var queryExpression = _context.Markings
            .OrderBy(x => x.UpdatedAt) as IQueryable<Marking>;

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Marking>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<Marking> GetMarkingAsync(Guid markingId)
    {
        if (markingId == Guid.Empty)
            throw new EmptyGuidException(nameof(markingId));

        return await _context.Markings
            .Where(x => x.Id == markingId)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(markingId));
    }

    public async Task AddMarkingAsync(Marking marking)
    {
        await _context.Markings.AddAsync(marking);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
