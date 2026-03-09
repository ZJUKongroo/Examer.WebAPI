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
[Route("api/user/detail")]
public class UserDetailController(IUserDetailRepository userDetailRepository, IMapper mapper) : ControllerBase
{
    private readonly IUserDetailRepository _userDetailRepository = userDetailRepository;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "Administrator")]
    [HttpGet(Name = nameof(GetUserDetails))]
    [EndpointDescription("获取所有用户个人信息")]
    public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetUserDetails([FromQuery] UserDetailDtoParameter parameter)
    {
        var userDetails = await _userDetailRepository.GetUserDetailsAsync(parameter);

        Response.Headers.AppendPaginationHeader(userDetails, parameter, Url, nameof(GetUserDetails));

        var userDetailDtos = _mapper.Map<IEnumerable<UserDetailDto>>(userDetails);
        return Ok(userDetailDtos);
    }

    [Authorize(Roles = "Student")]
    [HttpGet("me")]
    [EndpointDescription("查询自己的个人信息")]
    public async Task<ActionResult<UserDetailDto>> GetMyDetail()
    {
        try
        {
            Guid userId = Guid.Parse(HttpContext.User.Identity!.Name!);
            var userDetail = await _userDetailRepository.GetUserDetailAsync(userId);
            return Ok(_mapper.Map<UserDetailDto>(userDetail));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "Student")]
    [HttpPut("me")]
    [EndpointDescription("更新自己的个人信息")]
    public async Task<IActionResult> UpdateMyDetail(UpdateUserDetailDto updateUserDto)
    {
        try
        {
            Guid userId = Guid.Parse(HttpContext.User.Identity!.Name!);
            var userDetail = await _userDetailRepository.GetUserDetailAsync(userId);
            _mapper.Map(updateUserDto, userDetail);
            userDetail.UpdatedAt = DateTime.Now;
            return await _userDetailRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpPost]
    [EndpointDescription("新增用户信息")]
    public async Task<ActionResult<UserDetailDto>> AddUserDetail(AddUserDetailDto addUserDetailDto)
    {
        var userDetail = _mapper.Map<UserDetail>(addUserDetailDto);
        userDetail.UserId = Guid.Parse(HttpContext.User.Identity!.Name!);

        await _userDetailRepository.AddUserDetailAsync(userDetail);
        return await _userDetailRepository.SaveAsync() ? Created(nameof(GetMyDetail), _mapper.Map<UserDetailDto>(userDetail)) : Problem();
    }

    [Authorize]
    [HttpPut("{userId}")]
    [EndpointDescription("更改用户信息")]
    public async Task<IActionResult> UpdateUserDetail(Guid userId, UpdateUserDetailDto updateUserDto)
    {
        try
        {
            var user = await _userDetailRepository.GetUserDetailAsync(userId);

            _mapper.Map(updateUserDto, user);
            user.UpdatedAt = DateTime.Now;
            return await _userDetailRepository.SaveAsync() ? NoContent() : Problem();
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

    [Authorize]
    [HttpDelete("{userId}")]
    [EndpointDescription("删除个人信息")]
    public async Task<IActionResult> DeleteUserDetail(Guid userId)
    {
        try
        {
            var userDetail = await _userDetailRepository.GetUserDetailAsync(userId);

            userDetail.DeletedAt = DateTime.Now;

            return await _userDetailRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
    }
}
