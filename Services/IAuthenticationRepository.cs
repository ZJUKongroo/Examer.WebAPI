// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Dtos;
using Examer.Models;

namespace Examer.Services;

public interface IAuthenticationRepository
{
    Task<LoginResponseDto> LoginAsync(string studentNo, string password);
    Task RegisterAsync(User user);
    void SendEmailAsync(User user);
    Task ActivateAsync(Guid emailActivateToken);
    Task<bool> SaveAsync();
}
