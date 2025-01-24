using AutoMapper;
using Examer.Services;
using Examer.Dtos;
using Examer.DtoParameters;
using Examer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetUsers))]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserDtoParameter parameter)
    {
        var users = await _userRepository.GetUsersAsync(parameter);

        Response.Headers.AppendPaginationHeader(users, parameter, Url, nameof(GetUsers));

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(userDtos);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserDto>> GetUserInfo(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(userId);

            return Ok(_mapper.Map<UserDto>(user));
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUserInfo(Guid userId, UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(userId);

            _mapper.Map(updateUserDto, user);
            user.UpdateTime = DateTime.Now;
            return await _userRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUserInfo(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(userId);

            user.DeleteTime = DateTime.Now;
            user.IsDeleted = true;
            return await _userRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }
}
