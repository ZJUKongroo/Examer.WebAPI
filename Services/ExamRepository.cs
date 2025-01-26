using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class ExamRepository(ExamerDbContext context) : IExamRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<Exam>> GetExamsAsync(ExamDtoParameter parameter)
    {
        var queryExpression = _context.Exams!
            .OrderBy(x => x.StartTime)
            .Include(x => x.Problems) as IQueryable<Exam>;
        
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<Exam>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<Exam> GetExamAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        var exam = await _context.Exams!
            .Where(x => x.Id == examId)
            .Include(x => x.Problems)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examId));

        return exam;
    }

    public async Task AddExamAsync(Exam exam)
    {
        ArgumentNullException.ThrowIfNull(exam);

        await _context.Exams!.AddAsync(exam);
    }

    public async Task AddExamToUsersAsync(UserExam userExam)
    {
        ArgumentNullException.ThrowIfNull(userExam);
        
        await _context.UserExams!.AddAsync(userExam);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
