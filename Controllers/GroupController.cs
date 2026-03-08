// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.DtoParameters;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/group")]
[Authorize(Roles = "Administrator")]
public class GroupController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetGroups))]
    [EndpointDescription("获取所有队伍 可任意分页和筛选")]
    public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups([FromQuery] GroupDtoParameter parameter)
    {
        try
        {
            var groups = await _userRepository.GetGroupsAsync(parameter);

            Response.Headers.AppendPaginationHeader(groups, parameter, Url, nameof(GetGroups));

            var groupDtos = _mapper.Map<IEnumerable<GroupDto>>(groups);
            return Ok(groupDtos);
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpGet("{groupId}", Name = nameof(GetGroup))]
    [EndpointDescription("根据groupId获取队伍")]
    public async Task<ActionResult<GroupWithUsersAndExamIdsDto>> GetGroup(Guid groupId)
    {
        try
        {
            var group = await _userRepository.GetGroupAsync(groupId);

            var groupDto = _mapper.Map<GroupWithUsersAndExamIdsDto>(group);
            return Ok(groupDto);
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

    [HttpPost]
    [EndpointDescription("添加队伍")]
    public async Task<ActionResult<GroupDto>> AddGroup(AddGroupDto groupDto)
    {
        try
        {
            var group = _mapper.Map<User>(groupDto);

            group.Role = Role.Group;
            await _userRepository.AddUserAsync(group);
            return await _userRepository.SaveAsync() ? CreatedAtRoute(nameof(GetGroup), new { groupId = group.Id }, _mapper.Map<GroupDto>(group)) : Problem();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpPut("{groupId}")]
    [EndpointDescription("更新队伍信息")]
    public async Task<IActionResult> UpdateGroup(Guid groupId, UpdateGroupDto updateGroupDto)
    {
        try
        {
            var group = await _userRepository.GetGroupAsync(groupId);

            _mapper.Map(updateGroupDto, group);
            group.UpdatedAt = DateTime.Now;
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

    [HttpDelete("{groupId}")]
    [EndpointDescription("删除队伍")]
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        try
        {
            var group = await _userRepository.GetGroupAsync(groupId);

            group.DeletedAt = DateTime.Now;
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

    [HttpPost("distribution/{groupId}")]
    [EndpointDescription("把students添加进一个队伍")]
    public async Task<IActionResult> AddUsersToGroup(Guid groupId, IEnumerable<Guid> userIds)
    {
        try
        {
            await _userRepository.GetGroupAsync(groupId);
            foreach (var userId in userIds)
            {
                if (!await _userRepository.UserExistsAsync(userId))
                    continue;

                var group = new Group
                {
                    GroupId = groupId,
                    UserOfGroupId = userId
                };
                await _userRepository.AddUserToGroupAsync(group);
            }
            return await _userRepository.SaveAsync() ? Created() : Problem();
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

    [HttpDelete("distribution/{groupId}")]
    [EndpointDescription("把students从一个队伍删除")]
    public async Task<IActionResult> DeleteUserGroup(Guid groupId, IEnumerable<Guid> userIds)
    {
        try
        {
            await _userRepository.GetGroupAsync(groupId);
            foreach (var userId in userIds)
            {
                if (!await _userRepository.UserExistsAsync(userId))
                    continue;

                var userGroup = await _userRepository.GetUserGroupAsync(userId, groupId);
                userGroup.DeletedAt = DateTime.Now;
            }
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
