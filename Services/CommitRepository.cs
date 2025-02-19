using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class CommitRepository(ExamerDbContext context) : ICommitRepository
{
    private readonly static string filePathBase = "files/commits/"; // This field should be written to the configuration files
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

    public async Task<MemoryStream> GetCommitFileAsync(Guid commitId)
    {
        if (commitId == Guid.Empty)
            throw new ArgumentNullException(nameof(commitId));

        var commit = await GetCommitAsync(commitId);

        var stream = new MemoryStream();
        byte[] fileContent = await File.ReadAllBytesAsync(GetFilePath(commit.UserExam.ExamId, commit.ProblemId, commit.UserExam.UserId, commitId));
        await stream.WriteAsync(fileContent);
        stream.Position = 0;

        return stream;
    }

    public async Task AddCommitAsync(Commit commit)
    {
        ArgumentNullException.ThrowIfNull(commit);

        Directory.CreateDirectory(Path.GetDirectoryName(GetFilePath(commit.UserExam.ExamId, commit.ProblemId, commit.UserExam.UserId, commit.Id))!);
        
        commit.StorageLocation = GetFilePath(commit.UserExam.ExamId, commit.ProblemId, commit.UserExam.UserId, commit.Id);
        await _context.Commits!.AddAsync(commit);
    }

    public async Task AddCommitFileAsync(Commit commit, IFormFile formFile)
    {
        ArgumentNullException.ThrowIfNull(commit);
        ArgumentNullException.ThrowIfNull(formFile);

        using var stream = File.Create(GetFilePath(commit.UserExam.ExamId, commit.ProblemId, commit.UserExam.UserId, commit.Id));
        await formFile.CopyToAsync(stream);
    }

    public void DeleteCommitFile(Commit commit)
    {
        ArgumentNullException.ThrowIfNull(commit);

        File.Delete(GetFilePath(commit.UserExam.ExamId, commit.ProblemId, commit.UserExam.UserId, commit.Id));
    }

    public async Task<bool> CommitExistsAsync(Guid commitId)
    {
        if (commitId == Guid.Empty)
            throw new ArgumentNullException(nameof(commitId));

        var commit = await _context.Commits!
            .Where(x => x.Id == commitId)
            .Include(x => x.UserExam)
            .Include(x => x.UserExam)
            .FirstOrDefaultAsync();

        return (commit != null) && File.Exists(GetFilePath(commit.UserExam.ExamId, commit.ProblemId, commit.UserExam.UserId, commitId));
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    private static string GetFilePath(Guid examId, Guid problemId, Guid userId, Guid commitId)
    {
        return Path.GetFullPath(filePathBase + examId.ToString() + "/" + problemId.ToString() + "/" + userId.ToString() + "/" + commitId.ToString() + ".pdf");
    }
}
