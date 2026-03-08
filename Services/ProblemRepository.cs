// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Database;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class ProblemRepository(ExamerDbContext context) : IProblemRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task AddProblemAsync(Problem problem)
    {
        await _context.Problems.AddAsync(problem);
    }

    public async Task<Problem> GetProblemAsync(Guid problemId)
    {
        if (problemId == Guid.Empty)
            throw new EmptyGuidException(nameof(problemId));

        var problem = await _context.Problems
            .Where(x => x.Id == problemId)
            .Include(x => x.Files)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(problemId));

        return problem;
    }

    public async Task<bool> ProblemExistsAsync(Guid problemId)
    {
        if (problemId == Guid.Empty)
            throw new EmptyGuidException(nameof(problemId));

        return await _context.Problems
            .AnyAsync(x => x.Id == problemId);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
