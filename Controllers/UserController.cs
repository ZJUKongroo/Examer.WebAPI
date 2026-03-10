// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.DtoParameters;
using Examer.Dtos;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "Administrator, Manager")]
    [HttpGet(Name = nameof(GetUsers))]
    [EndpointDescription("获取所有用户 可任意分页和筛选")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserDtoParameter parameter)
    {
        var users = await _userRepository.GetUsersAsync(parameter);

        Response.Headers.AppendPaginationHeader(users, parameter, Url, nameof(GetUsers));

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(userDtos);
    }

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpGet("groups/{userId}", Name = nameof(GetGroupsByUserId))]
    [EndpointDescription("获取用户所在组")]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsByUserId(Guid userId, [FromQuery] GroupWithExamIdDtoParameter parameter)
    {
        try
        {
            var groups = await _userRepository.GetGroupsByUserIdAsync(parameter, userId);

            Response.Headers.AppendPaginationHeader(groups, parameter, Url, nameof(GetGroupsByUserId));

            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);
            return Ok(groupDtos);
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpGet("{userId}", Name = nameof(GetUser))]
    [EndpointDescription("根据userId获取用户")]
    public async Task<ActionResult<UserWithExamIdsDto>> GetUser(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(userId);

            return Ok(_mapper.Map<UserWithExamIdsDto>(user));
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [EndpointDescription("添加用户 请注意：不要在前端生产环境中使用 此接口仅为导入数据脚本保留")]
    public async Task<ActionResult<UserDto>> AddUser(AddUserDto addUserDto)
    {
        try
        {
            var user = _mapper.Map<User>(addUserDto);

            await _userRepository.AddUserAsync(user);
            return await _userRepository.SaveAsync() ? CreatedAtRoute(nameof(GetUser), new { userId = user.Id }, _mapper.Map<UserDto>(user)) : Problem();
        }
        catch (NotUniqueException)
        {
            return Conflict();
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{userId}")]
    [EndpointDescription("删除用户 请注意：不要在前端生产环境中使用 此接口仅为测试保留")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(userId);

            user.DeletedAt = DateTime.UtcNow;

            return await _userRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
