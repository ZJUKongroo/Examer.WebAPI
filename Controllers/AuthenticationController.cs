// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(IAuthenticationRepository authenticationRepository, IMapper mapper) : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost("login")]
    [EndpointDescription("登录接口")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginDto)
    {
        try
        {
            var loginResponseDto = await _authenticationRepository.LoginAsync(loginDto.StudentNumber, loginDto.Password);

            if (loginResponseDto == null)
                return Unauthorized();

            return Ok(loginResponseDto);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("register")]
    [EndpointDescription("注册接口")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        try
        {
            var user = _mapper.Map<RegisterDto, User>(registerDto);
            user.Role = Role.Student;

            await _authenticationRepository.RegisterAsync(user);
            bool success = await _authenticationRepository.SaveAsync();
            await _authenticationRepository.SendEmailAsync(user, EmailType.ActivateAccountEmail);

            return success ? NoContent() : Problem();
        }
        catch (NotUniqueException)
        {
            return Conflict();
        }
        catch (NotSupportedException)
        {
            return Problem();
        }
    }

    [HttpPost("activate/{emailActivateToken}")]
    [EndpointDescription("激活用户接口")]
    public async Task<ActionResult<LoginResponseDto>> Activate(Guid emailActivateToken)
    {
        try
        {
            var loginResponseDto = await _authenticationRepository.ActivateAsync(emailActivateToken);
            return await _authenticationRepository.SaveAsync() ? Ok(loginResponseDto) : Problem();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("reset/{studentNumber}")]
    [EndpointDescription("重置密码发送邮件接口")]
    public async Task<IActionResult> Reset([Required, Length(10, 10), RegularExpression(@"^\d{10}$")] string studentNumber)
    {
        try
        {
            var user = await _authenticationRepository.GetUserByStudentNumberAsync(studentNumber);
            user.ResetPasswordToken = Guid.NewGuid();
            bool success = await _authenticationRepository.SaveAsync();
            await _authenticationRepository.SendEmailAsync(user, EmailType.ResetPasswordEmail);

            return success ? NoContent() : Problem();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (NotSupportedException)
        {
            return Problem();
        }
    }

    [HttpPut("password")]
    [EndpointDescription("重置密码接口")]
    public async Task<ActionResult<LoginResponseDto>> UpdatePassword(ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var loginResponseDto = await _authenticationRepository.UpdatePasswordAsync(resetPasswordDto.PasswordResetToken, resetPasswordDto.Password);
            return await _authenticationRepository.SaveAsync() ? Ok(loginResponseDto) : Problem();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
