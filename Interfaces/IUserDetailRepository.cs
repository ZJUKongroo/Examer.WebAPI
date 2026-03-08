// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Interfaces;

public interface IUserDetailRepository
{
    Task<PagedList<UserDetail>> GetUserDetailsAsync(UserDetailDtoParameter parameter);
    Task<UserDetail> GetUserDetailAsync(Guid userId);
    Task AddUserDetailAsync(UserDetail userDetail);
    Task<bool> SaveAsync();
}
