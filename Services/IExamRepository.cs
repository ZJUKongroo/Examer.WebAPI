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
    Task<UserExam> GetUserExamAsync(Guid userId, Guid examId);
    Task<Exam> GetExamWithUserOrGroupsAsync(Guid examId);
    Task<bool> SaveAsync();
}
