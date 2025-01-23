using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class UserRepository(ExamerDbContext context) : IUserRepository
{
    private readonly ExamerDbContext _context = context;
    
    public async Task<PagedList<User>> GetUsersAsync(UserDtoParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var queryExpression = _context.Users!.OrderBy(x => x.StudentNo) as IQueryable<User>;
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<User> GetUserAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        var userInfo = await _context.Users!
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(userId));

        return userInfo;
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
