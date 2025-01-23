using AutoMapper;
using Examer.Services;
using Examer.Dtos;
using Examer.Enums;
using Examer.DtoParameters;
using Examer.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetUsers))]
    // [Authorize(Roles = "Administrator, Manager, Student")] // Only for test, delete this line before publishing
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserDtoParameter parameter)
    {
        var users = await _userRepository.GetUsersAsync(parameter);

        var previousPageLink = users.HasPrevious ? CreateUsersResourceUri(parameter, ResourceUriType.PreviousPage) : null;
        var nextPageLink = users.HasNext ? CreateUsersResourceUri(parameter, ResourceUriType.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = users.TotalCount,
            pageSize = users.PageSize,
            currentPage = users.CurrentPage,
            totalPages = users.TotalPages,
            previousPageLink,
            nextPageLink
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetadata, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        }));

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
            user.ModifyTime = DateTime.Now;
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

    private string CreateUsersResourceUri(UserDtoParameter parameter, ResourceUriType type)
    {
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                return Url.Link(nameof(GetUsers), new
                {
                    pageNumber = parameter.PageNumber - 1,
                    pageSize = parameter.PageSize,
                })!;
            case ResourceUriType.NextPage:
                return Url.Link(nameof(GetUsers), new
                {
                    pageNumber = parameter.PageNumber + 1,
                    pageSize = parameter.PageSize,
                })!;
            default:
                return Url.Link(nameof(GetUsers), new{
                    pageNumber = parameter.PageNumber,
                    pageSize = parameter.PageSize
                })!;
        }
    }
}
