using Examer.DtoParameters;
using Examer.Models;
using Examer.Helpers;

namespace Examer.Services;

public interface IExamRepository
{
    Task<PagedList<Exam>> GetExamsAsync(ExamDtoParameter parameter);
    Task<Exam> GetExamAsync(Guid examId);
    Task AddExamAsync(Exam exam);
    Task AddExamToUsersAsync(UserExam userExam);
    Task<bool> SaveAsync();
}
