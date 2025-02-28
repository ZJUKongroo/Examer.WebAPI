using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Services;

public interface IUserRepository
{
    Task<PagedList<User>> GetUsersAsync(UserDtoParameter parameter);
    Task<User> GetUserAsync(Guid userId);
    Task<PagedList<User>> GetGroupsAsync(GroupDtoParameter parameter);
    Task<PagedList<User>> GetGroupsByUserIdAsync(GroupDtoParameter parameter, Guid userId);
    Task<User> GetGroupAsync(Guid groupId);
    Task AddUserAsync(User user);
    Task<bool> UserExistsAsync(Guid userId);
    Task<bool> GroupExistsAsync(Guid groupId);
    Task<bool> UserOrGroupExistsAsync(Guid userOrGroupId);
    Task AddUserToGroupAsync(Group group);
    Task<Group> GetUserGroupAsync(Guid userId, Guid groupId);
    Task<bool> SaveAsync();
}
