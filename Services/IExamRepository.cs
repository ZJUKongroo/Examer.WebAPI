using Examer.Models;

namespace Examer.Services;

public interface IExamRepository
{
    Task<IEnumerable<Exam>> GetExamsAsync();
    Task<Exam> GetExamAsync(Guid examId);
    Task AddExamAsync(Exam exam);
    Task AddExamToUsersAsync(Guid examId, IEnumerable<Guid> userIds);
    Task<bool> SaveAsync();
}
