// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Dtos;

namespace Examer.Services;

public interface IAuthenticationRepository
{
    Task<LoginDto> LoginAsync(string studentNo, string password);
}
