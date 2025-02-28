using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class CommitRepository(ExamerDbContext context) : ICommitRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<Commit>> GetCommitsAsync(CommitDtoParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var queryExpression = _context.Commits!
            .Include(x => x.UserExam)
            .ThenInclude(x => x.User)
            .Include(x => x.UserExam)
            .ThenInclude(x => x.Exam)
            .Include(x => x.Problem)
            .Include(x => x.Markings)
            .Include(x => x.Files)
            .OrderBy(x => x.UserExam.User.StudentNo) as IQueryable<Commit>;

        // Temporary filtering solution
        queryExpression = queryExpression.Where(x => x.UserExam.UserId == parameter.UserId);
        queryExpression = queryExpression.Where(x => x.UserExam.ExamId == parameter.ExamId);
        
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Commit>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<Commit> GetCommitAsync(Guid commitId)
    {
        if (commitId == Guid.Empty)
            throw new ArgumentNullException(nameof(commitId));

        var commit = await _context.Commits!
            .Where(x => x.Id == commitId)
            .Include(x => x.UserExam)
            .ThenInclude(x => x.User)
            .Include(x => x.UserExam)
            .ThenInclude(x => x.Exam)
            .Include(x => x.Problem)
            .Include(x => x.Markings)
            .Include(x => x.Files)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(commitId));
        
        return commit;
    }

    public async Task AddCommitAsync(Commit commit)
    {
        ArgumentNullException.ThrowIfNull(commit);
     
        await _context.Commits!.AddAsync(commit);
    }

    public async Task<bool> CommitExistsAsync(Guid commitId)
    {
        if (commitId == Guid.Empty)
            throw new ArgumentNullException(nameof(commitId));

        return await _context.Commits!
            .AnyAsync(x => x.Id == commitId);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
