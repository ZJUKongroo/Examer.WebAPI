// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Database;
using Examer.DtoParameters;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

using Org.BouncyCastle.Crypto.Paddings;

namespace Examer.Services;

public class UserRepository(ExamerDbContext context) : IUserRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<User>> GetUsersAsync(UserDtoParameter parameter)
    {
        var queryExpression = _context.Users
            .Where(x => x.Role == Role.Student)
            .OrderBy(x => x.StudentNumber) as IQueryable<User>;

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<User> GetUserAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new EmptyGuidException(nameof(userId));

        var user = await _context.Users
            .Where(x => x.Role == Role.Student)
            .Where(x => x.Id == userId)
            .Include(x => x.Exams)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(userId));

        return user;
    }

    public async Task<PagedList<User>> GetGroupsAsync(GroupDtoParameter parameter)
    {
        var queryExpression = _context.Users
            .Where(x => x.Role == Role.Group)
            .OrderBy(x => x.Name)
            .Include(x => x.UsersOfGroup) as IQueryable<User>;
        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<PagedList<User>> GetGroupsByUserIdAsync(GroupWithExamIdDtoParameter parameter, Guid userId)
    {
        if (userId == Guid.Empty)
            throw new EmptyGuidException(nameof(userId));

        var queryExpression = _context.Users
            .Where(x => x.Role == Role.Group)
            .OrderBy(x => x.Name)
            .Include(x => x.Groups)
            .Where(x => x.Groups.Any(x => x.UserOfGroupId == userId));

        if (parameter.ExamId != Guid.Empty)
            queryExpression = queryExpression
                .Include(x => x.UserExams)
                .Where(x => x.UserExams.Any(x => x.ExamId == parameter.ExamId));

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<User>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<User> GetGroupAsync(Guid groupId)
    {
        if (groupId == Guid.Empty)
            throw new EmptyGuidException(nameof(groupId));

        var group = await _context.Users
            .Where(x => x.Role == Role.Group)
            .Where(x => x.Id == groupId)
            .Include(x => x.UsersOfGroup)
            .Include(x => x.Exams)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(groupId));

        return group;
    }

    public async Task AddUserAsync(User user)
    {
        if (_context.Users.Any(x => x.StudentNumber == user.StudentNumber && x.Role != Role.Group))
            throw new NotUniqueException();

        await _context.AddAsync(user);
    }

    public async Task<bool> UserExistsAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new EmptyGuidException(nameof(userId));

        return await _context.Users
            .Where(x => x.Role == Role.Student)
            .AnyAsync(x => x.Id == userId);
    }

    public async Task<bool> GroupExistsAsync(Guid groupId)
    {
        if (groupId == Guid.Empty)
            throw new EmptyGuidException(nameof(groupId));

        return await _context.Users
            .Where(x => x.Role == Role.Group)
            .AnyAsync(x => x.Id == groupId);
    }

    public async Task<bool> UserOrGroupExistsAsync(Guid userOrGroupId)
    {
        if (userOrGroupId == Guid.Empty)
            throw new EmptyGuidException(nameof(userOrGroupId));

        return await _context.Users
            .Where(x => x.Role == Role.Student || x.Role == Role.Group)
            .AnyAsync(x => x.Id == userOrGroupId);
    }

    public async Task AddUserToGroupAsync(Group group)
    {
        await _context.Groups.AddAsync(group);
    }

    public async Task<Group> GetUserGroupAsync(Guid userId, Guid groupId)
    {
        if (userId == Guid.Empty)
            throw new EmptyGuidException(nameof(userId));
        if (groupId == Guid.Empty)
            throw new EmptyGuidException(nameof(groupId));

        return await _context.Groups
            .Where(x => x.UserOfGroupId == userId)
            .Where(x => x.GroupId == groupId)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(userId) + nameof(groupId));
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
