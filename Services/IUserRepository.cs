using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Services;

public interface IUserRepository
{
    Task<PagedList<User>> GetUsersAsync(UserDtoParameter parameter);
    Task<User> GetUserAsync(Guid userId);
    Task<bool> SaveAsync();
}
