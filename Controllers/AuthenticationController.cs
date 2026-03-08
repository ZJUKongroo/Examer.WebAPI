// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.Dtos;
using Examer.Models;
using Examer.Services;
using Examer.Enums;

using Microsoft.AspNetCore.Mvc;
using Examer.Helpers;

namespace Examer.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController(IAuthenticationRepository authenticationRepository, IMapper mapper) : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost("login")]
    [EndpointDescription("登录接口")]
    public async Task<ActionResult<LoginDto>> Login(LoginDto loginDto)
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
        catch (NullReferenceException)
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
            _authenticationRepository.SendEmailAsync(user);

            return success ? NoContent() : Problem();
        }
        catch (NotUniqueException)
        {
            return Conflict();
        }
    }

    [HttpPost("activate/{emailActivateToken}")]
    [EndpointDescription("激活接口")]
    public async Task<IActionResult> Activate(Guid emailActivateToken)
    {
        try
        {
            await _authenticationRepository.ActivateAsync(emailActivateToken);

            return await _authenticationRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }
}
