// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Dtos;

namespace Examer.Services;

public interface IAuthenticationRepository
{
    Task<LoginResponseDto> LoginAsync(string studentNo, string password);
    Task RegisterAsync();
    Task ActivateAsync();
}
