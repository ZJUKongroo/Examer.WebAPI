// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Database;
using Examer.DtoParameters;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class ExamRepository(ExamerDbContext context) : IExamRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<Exam>> GetExamsAsync(ExamDtoParameter parameter)
    {
        var queryExpression = _context.Exams
            .OrderBy(x => x.StartTime)
            .Include(x => x.Problems) as IQueryable<Exam>;

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Exam>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<PagedList<Exam>> GetExamsForStudentAsync(ExamDtoParameter parameter, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        var queryExpression = _context.Exams
            .Include(x => x.UserExams.Where(x => x.UserId == userId))
            .Where(x => x.StartTime <= DateTime.Now)
            .Where(x => x.EndTime >= DateTime.Now)
            .OrderBy(x => x.StartTime)
            .Include(x => x.Problems) as IQueryable<Exam>;

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Exam>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<Exam> GetExamAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        var exam = await _context.Exams
            .Where(x => x.Id == examId)
            .Include(x => x.Problems)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examId));

        return exam;
    }

    public async Task AddExamAsync(Exam exam)
    {
        await _context.Exams.AddAsync(exam);
    }

    public async Task<bool> ExamExistsAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        return await _context.Exams
            .AnyAsync(x => x.Id == examId);
    }

    public async Task AddExamToUsersAsync(UserExam userExam)
    {
        await _context.UserExams.AddAsync(userExam);
    }

    public async Task<UserExam> GetUserExamAsync(Guid userId, Guid examId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        return await _context.UserExams
            .Where(x => x.UserId == userId)
            .Where(x => x.ExamId == examId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(userId) + nameof(examId));
    }

    public async Task<Exam> GetExamWithUserOrGroupsAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        return await _context.Exams
            .Where(x => x.Id == examId)
            .Include(x => x.Users)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examId));
    }
    public async Task<Exam> GetExamWithUsersAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        return await _context.Exams
            .Where(x => x.Id == examId)
            .Include(x => x.Users.Where(x => x.Role != Role.Group))
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examId));
    }
    public async Task<Exam> GetExamWithGroupsAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        return await _context.Exams
            .Where(x => x.Id == examId)
            .Include(x => x.Users.Where(x => x.Role == Role.Group))
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examId));
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
