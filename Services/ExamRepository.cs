using Examer.Database;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class ExamRepository(ExamerDbContext context) : IExamRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<IEnumerable<Exam>> GetExamsAsync()
    {
        var exams = await _context.Exams!.ToListAsync();

        return exams;
    }

    public async Task<Exam> GetExamAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        var exam = await _context.Exams!
            .Where(x => x.Id == examId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examId));

        return exam;
    }

    public async Task AddExamAsync(Exam exam)
    {
        ArgumentNullException.ThrowIfNull(exam);

        await _context.Exams!.AddAsync(exam);
    }

    public async Task AddExamToUsersAsync(Guid examId, IEnumerable<Guid> userIds)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));
        
        if (!userIds.Any())
            throw new ArgumentNullException(nameof(userIds));
        
        foreach (Guid userId in userIds)
        {
            var userInfo = await _context.Users!
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(userIds));
        }
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
