// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Dtos;
using Examer.Enums;
using Examer.Models;

namespace Examer.Interfaces;

public interface IAuthenticationRepository
{
    Task<LoginResponseDto> LoginAsync(string studentNumber, string password);
    Task RegisterAsync(User user);
    Task SendEmailAsync(User user, EmailType emailType);
    Task<LoginResponseDto> ActivateAsync(Guid emailActivateToken);
    Task<User> GetUserByStudentNumberAsync(string studentNumber);
    Task<LoginResponseDto> UpdatePasswordAsync(Guid passwordResetToken, string password);
    Task<bool> SaveAsync();
}
