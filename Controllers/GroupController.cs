using AutoMapper;
using Examer.DtoParameters;
using Examer.Dtos;
using Examer.Models;
using Examer.Helpers;
using Examer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetGroups))]
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

    [HttpGet("{groupId}")]
    public async Task<ActionResult<GroupDto>> GetGroup(Guid groupId)
    {
        try
        {
            var group = await _userRepository.GetGroupAsync(groupId);

            var groupDto = _mapper.Map<GroupDto>(group);
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
    public async Task<IActionResult> AddGroup(AddGroupDto groupDto)
    {
        try
        {
            var group = _mapper.Map<User>(groupDto);

            group.CreateTime = DateTime.Now;
            group.UpdateTime = DateTime.Now;
            await _userRepository.AddUserAsync(group);
            return await _userRepository.SaveAsync() ? Created() : Problem();
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
    }

    [HttpPut("{groupId}")]
    public async Task<IActionResult> UpdateGroup(Guid groupId, UpdateGroupDto updateGroupDto)
    {
        try
        {
            var group = await _userRepository.GetGroupAsync(groupId);

            _mapper.Map(updateGroupDto, group);
            group.UpdateTime = DateTime.Now;
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
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        try
        {
            var group = await _userRepository.GetGroupAsync(groupId);

            group.DeleteTime = DateTime.Now;
            group.IsDeleted = true;
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

    [HttpPost("{groupId}")]
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
                    UserOfGroupId = userId,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
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
}
