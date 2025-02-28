using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Examer.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Examer.Services;

public class UserRepository(ExamerDbContext context) : IUserRepository
{
    private readonly ExamerDbContext _context = context;
    
    public async Task<PagedList<User>> GetUsersAsync(UserDtoParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var queryExpression = _context.Users!
            .Where(x => x.Role == Role.Student)
            .OrderBy(x => x.StudentNo) as IQueryable<User>;
    
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<User> GetUserAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        var user = await _context.Users!
            .Where(x => x.Role == Role.Student)
            .Where(x => x.Id == userId)
            .Include(x => x.Exams)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(userId));

        return user;
    }

    public async Task<PagedList<User>> GetGroupsAsync(GroupDtoParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var queryExpression = _context.Users!
            .Where(x => x.Role == Role.Group)
            .OrderBy(x => x.Name)
            .Include(x => x.UsersOfGroup) as IQueryable<User>;
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<PagedList<User>> GetGroupsByUserIdAsync(GroupDtoParameter parameter, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        var queryExpression = _context.Users!
            .Where(x => x.Role == Role.Group)
            .OrderBy(x => x.Name)
            .Include(x => x.UsersOfGroup.Where(x => x.Id == userId)) as IQueryable<User>;
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<User> GetGroupAsync(Guid groupId)
    {
        if (groupId == Guid.Empty)
            throw new ArgumentNullException(nameof(groupId));
        
        var group = await _context.Users!
            .Where(x => x.Role == Role.Group)
            .Where(x => x.Id == groupId)
            .Include(x => x.UsersOfGroup)
            .Include(x => x.Exams)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(groupId));
        
        return group;
    }

    public async Task AddUserAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        await _context.AddAsync(user);
    }

    public async Task<bool> UserExistsAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));

        return await _context.Users!
            .Where(x => x.Role == Role.Student)
            .AnyAsync(x => x.Id == userId);
    }

    public async Task<bool> GroupExistsAsync(Guid groupId)
    {
        if (groupId == Guid.Empty)
            throw new ArgumentNullException(nameof(groupId));
        
        return await _context.Users!
            .Where(x => x.Role == Role.Group)
            .AnyAsync(x => x.Id == groupId);
    }
    
    public async Task<bool> UserOrGroupExistsAsync(Guid userOrGroupId)
    {
        if (userOrGroupId == Guid.Empty)
            throw new ArgumentNullException(nameof(userOrGroupId));
        
        return await _context.Users!
            .Where(x => x.Role == Role.Student || x.Role == Role.Group)
            .AnyAsync(x => x.Id == userOrGroupId);
    }

    public async Task AddUserToGroupAsync(Group group)
    {
        ArgumentNullException.ThrowIfNull(group);
        
        await _context.Groups!.AddAsync(group);
    }

    public async Task<Group> GetUserGroupAsync(Guid userId, Guid groupId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentNullException(nameof(userId));
        if (groupId == Guid.Empty)
            throw new ArgumentNullException(nameof(groupId));

        return await _context.Groups!
            .Where(x => x.UserOfGroupId == userId)
            .Where(x => x.GroupId == groupId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(userId) + nameof(groupId));
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
