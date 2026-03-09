// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Dtos;
using Examer.Models;

namespace Examer.Interfaces;

public interface IAuthenticationRepository
{
    Task<LoginResponseDto> LoginAsync(string studentNo, string password);
    Task RegisterAsync(User user);
    Task SendEmailAsync(User user);
    Task<LoginResponseDto> ActivateAsync(Guid emailActivateToken);
    Task<bool> SaveAsync();
}
