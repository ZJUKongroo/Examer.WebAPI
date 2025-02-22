using Examer.Models;

namespace Examer.Services;

public interface IProblemRepository
{
    Task AddProblemAsync(Problem problem);
    Task<Problem> GetProblemAsync(Guid problemId);
    Task<bool> ProblemExistsAsync(Guid problemId);
    Task<bool> SaveAsync();
}
