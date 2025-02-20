using Examer.Models;

namespace Examer.Services;

public interface IProblemRepository
{
    Task AddProblemAsync(Problem problem);
    Task AddProblemFileAsync(Problem problem, IFormFile formFile);
    Task<Problem> GetProblemAsync(Guid problemId);
    Task<MemoryStream> GetProblemFileAsync(Guid problemId);
    void DeleteProblemFile(Problem problem);
    Task<bool> ProblemExistsAsync(Guid problemId);
    Task<bool> ProblemWithFileExistsAsync(Guid problemId);
    Task<bool> SaveAsync();
}
