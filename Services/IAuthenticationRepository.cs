using Examer.Dtos;

namespace Examer.Services;

public interface IAuthenticationRepository
{
    Task<LoginDto> LoginAsync(string studentNo);
}
