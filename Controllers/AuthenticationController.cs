// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.Dtos;
using Examer.Services;

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
    public async Task<IActionResult> Register()
    {
        try
        {
            return Ok();
        }
        catch (NullReferenceException)
        {
            return Ok();
        }
    }

    [HttpPost("activate")]
    [EndpointDescription("激活接口")]
    public async Task<IActionResult> Activate()
    {
        try
        {
            return Ok();
        }
        catch (NullReferenceException)
        {
            return Ok();
        }
    }
}
