// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Services;

public class UserDetailRepository(ExamerDbContext context) : IUserDetailRepository
{
    private readonly ExamerDbContext _context = context;

    public async Task<PagedList<UserDetail>> GetUserDetailsAsync(UserDetailDtoParameter parameter)
    {
        var queryExpression = _context.UserDetails
            .Include(x => x.User)
            .OrderBy(x => x.User.StudentNumber) as IQueryable<UserDetail>;

        return await PagedList<UserDetail>.CreateAsync(queryExpression, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<UserDetail> GetUserDetailAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new EmptyGuidException(nameof(userId));

        return await _context.UserDetails
            .Where(x => x.UserId == userId)
            .Include(x => x.User)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(userId));
    }

    public async Task AddUserDetailAsync(UserDetail userDetail)
    {
        await _context.UserDetails.AddAsync(userDetail);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
