using Examer.Database;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class ProblemRepository(ExamerDbContext context) : IProblemRepository
{
    private readonly static string filePathBase = "files/problems/"; // This field should be written to the configuration files
    private readonly ExamerDbContext _context = context;

    public async Task AddProblemAsync(Problem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        Directory.CreateDirectory(Path.GetDirectoryName(GetFilePath(problem.ExamId, problem.Id))!);

        problem.StorageLocation = GetFilePath(problem.ExamId, problem.Id);
        await _context.Problems!.AddAsync(problem);
    }

    public async Task AddProblemFileAsync(Problem problem, IFormFile formFile)
    {
        ArgumentNullException.ThrowIfNull(problem);
        ArgumentNullException.ThrowIfNull(formFile);

        using var stream = File.Create(GetFilePath(problem.ExamId, problem.Id));
        await formFile.CopyToAsync(stream);
    }

    public async Task<Problem> GetProblemAsync(Guid problemId)
    {
        if (problemId == Guid.Empty)
            throw new ArgumentNullException(nameof(problemId));

        var problem = await _context.Problems!
            .Where(x => x.Id == problemId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(problemId));
        
        return problem;
    }

    public async Task<MemoryStream> GetProblemFileAsync(Guid problemId)
    {
        if (problemId == Guid.Empty)
            throw new ArgumentNullException(nameof(problemId));

        var problem = await GetProblemAsync(problemId);

        var stream = new MemoryStream();
        byte[] fileContent = await File.ReadAllBytesAsync(GetFilePath(problem.ExamId, problem.Id));
        await stream.WriteAsync(fileContent);
        stream.Position = 0;

        return stream;
    }

    public void DeleteProblemFile(Problem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        File.Delete(GetFilePath(problem.ExamId, problem.Id));
    }

    public async Task<bool> ProblemExistsAsync(Guid problemId)
    {
        if (problemId == Guid.Empty)
            throw new ArgumentNullException(nameof(problemId));

        return await _context.Problems!
            .AnyAsync(x => x.Id == problemId);
    }

    public async Task<bool> ProblemWithFileExistsAsync(Guid problemId)
    {
        if (problemId == Guid.Empty)
            throw new ArgumentNullException(nameof(problemId));
        
        var problem = await _context.Problems!
            .Where(x => x.Id == problemId)
            .FirstOrDefaultAsync();

        return (problem != null) && File.Exists(GetFilePath(problem.ExamId, problem.Id));
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    private static string GetFilePath(Guid examId, Guid problemId)
    {
        return Path.GetFullPath(filePathBase + examId.ToString() + "/" + problemId.ToString() + ".pdf");
    }
}
