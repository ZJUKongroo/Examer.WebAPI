using AutoMapper;
using Examer.Services;
using Examer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationRepository authenticationRepository, IMapper mapper) : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [EndpointDescription("登录接口，后续可能根据统一认证的接口有所更改")]
    public async Task<ActionResult<LoginDto>> Login(string studentNo, string password)
    {
        try
        {
            var loginDto = await _authenticationRepository.LoginAsync(studentNo, password);

            if (loginDto == null)
                return Unauthorized();

            return Ok(loginDto);
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
}
