// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Interfaces;

public interface IExamRepository
{
    Task<PagedList<Exam>> GetExamsAsync(ExamDtoParameter parameter);
    Task<PagedList<Exam>> GetExamsForStudentAsync(ExamDtoParameter parameter, Guid userId);
    Task<Exam> GetExamAsync(Guid examId);
    Task AddExamAsync(Exam exam);
    Task<bool> ExamExistsAsync(Guid examId);
    Task AddExamToUsersAsync(UserExam userExam);
    Task<UserExam> GetUserExamAsync(Guid userId, Guid examId);
    Task<Exam> GetExamWithUserOrGroupsAsync(Guid examId);
    Task<Exam> GetExamWithUsersAsync(Guid examId);
    Task<Exam> GetExamWithGroupsAsync(Guid examId);
    Task<bool> SaveAsync();
}
