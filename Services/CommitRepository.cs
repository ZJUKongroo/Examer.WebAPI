// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class CommitRepository(ExamerDbContext context) : ICommitRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<Commit>> GetCommitsAsync(CommitDtoParameter parameter)
    {
        var queryExpression = _context.Commits
            .Include(x => x.UserExam)
            .ThenInclude(x => x.User)
            .Include(x => x.UserExam)
            .ThenInclude(x => x.Exam)
            .Include(x => x.Problem)
            .Include(x => x.Markings)
            .Include(x => x.Files)
            .OrderBy(x => x.UserExam.User.StudentNumber) as IQueryable<Commit>;

        // Temporary filtering solution
        if (parameter.UserId != Guid.Empty)
            queryExpression = queryExpression.Where(x => x.UserExam.UserId == parameter.UserId);
        if (parameter.ExamId != Guid.Empty)
            queryExpression = queryExpression.Where(x => x.UserExam.ExamId == parameter.ExamId);

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Commit>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<Commit> GetCommitAsync(Guid commitId)
    {
        if (commitId == Guid.Empty)
            throw new EmptyGuidException(nameof(commitId));

        var commit = await _context.Commits
            .Where(x => x.Id == commitId)
            .Include(x => x.UserExam)
            .ThenInclude(x => x.User)
            .Include(x => x.UserExam)
            .ThenInclude(x => x.Exam)
            .Include(x => x.Problem)
            .Include(x => x.Markings)
            .Include(x => x.Files)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(commitId));

        return commit;
    }

    public async Task AddCommitAsync(Commit commit)
    {
        await _context.Commits.AddAsync(commit);
    }

    public async Task<bool> CommitExistsAsync(Guid commitId)
    {
        if (commitId == Guid.Empty)
            throw new EmptyGuidException(nameof(commitId));

        return await _context.Commits
            .AnyAsync(x => x.Id == commitId);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
