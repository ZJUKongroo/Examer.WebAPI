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
            .OrderBy(x => x.UserExam.User.StudentNo) as IQueryable<Commit>;
        
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
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(commitId));
        
        return commit;
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
