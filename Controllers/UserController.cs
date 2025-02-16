using AutoMapper;
using Examer.Services;
using Examer.Dtos;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class UserController(IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetUsers))]
    [EndpointDescription("获取所有用户 可任意分页和筛选 此控制器下均为Administrator权限")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserDtoParameter parameter)
    {
        try
        {
            var users = await _userRepository.GetUsersAsync(parameter);

            Response.Headers.AppendPaginationHeader(users, parameter, Url, nameof(GetUsers));

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }


    [HttpGet("{userId}", Name = nameof(GetUser))]
    [EndpointDescription("根据userId获取用户")]
    public async Task<ActionResult<UserWithExamIdsDto>> GetUser(Guid userId)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(userId);

            return Ok(_mapper.Map<UserWithExamIdsDto>(user));
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
    [EndpointDescription("添加用户 请注意：不要在前端生产环境中使用 此接口仅为导入数据脚本保留")]
    public async Task<IActionResult> AddUser(AddUserDto addUserDto)
    {
        try
        {
            var user = _mapper.Map<User>(addUserDto);

            user.Id = Guid.NewGuid();
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            await _userRepository.AddUserAsync(user);
            return await _userRepository.SaveAsync() ? CreatedAtRoute(nameof(GetUser), new { userId = user.Id }, _mapper.Map<UserDto>(user)) : Problem();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpPut("{userId}")]
    [EndpointDescription("更改用户信息")]
    public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserDto updateUserDto)
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
    [EndpointDescription("删除用户 请注意：不要在前端生产环境中使用 此接口仅为测试保留")]
    public async Task<IActionResult> DeleteUser(Guid userId)
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
