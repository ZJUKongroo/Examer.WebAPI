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
[Route("api/commit")]
public class CommitController(ICommitRepository commitRepository, IExamRepository examRepository, IMapper mapper) : ControllerBase
{
    private readonly ICommitRepository _commitRepository = commitRepository;
    private readonly IExamRepository _examRepository = examRepository;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpGet(Name = nameof(GetCommits))]
    [EndpointDescription("获取所有提交 可任意分页和筛选")]
    public async Task<ActionResult<IEnumerable<CommitDto>>> GetCommits([FromQuery] CommitDtoParameter parameter)
    {
        try
        {
            if (User.IsInRole("Student") && User.Identity!.Name! != parameter.UserId.ToString())
                return Forbid();

            var commits = await _commitRepository.GetCommitsAsync(parameter);

            Response.Headers.AppendPaginationHeader(commits, parameter, Url, nameof(GetCommits));

            IEnumerable<CommitDto> commitDtos = null!;
            if (User.IsInRole("Student"))
                commitDtos = _mapper.Map<IEnumerable<CommitDto>>(commits);
            else
                commitDtos = _mapper.Map<IEnumerable<CommitWithMarkingsDto>>(commits);
            return Ok(commitDtos);
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Administrator, Manager")]
    [HttpGet("{commitId}", Name = nameof(GetCommit))]
    [EndpointDescription("根据commitId获取用户")]
    public async Task<ActionResult<CommitWithMarkingsDto>> GetCommit(Guid commitId)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            return Ok(_mapper.Map<CommitWithMarkingsDto>(commit));
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

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpPost]
    [EndpointDescription("添加提交信息")]
    public async Task<ActionResult<CommitDto>> AddCommit(AddCommitDto addCommitDto)
    {
        try
        {
            // TODO: FIX IT when exam is public
            var commit = _mapper.Map<Commit>(addCommitDto);
            var exam = await _examRepository.GetExamAsync(addCommitDto.ExamId);

            UserExam userExam;
            try
            {
                userExam = await _examRepository.GetUserExamAsync(addCommitDto.UserId, addCommitDto.ExamId);
            }
            catch (NotFoundException)
            {
                if (!exam.IsPublic)
                    throw;

                userExam = new UserExam
                {
                    UserId = addCommitDto.UserId,
                    ExamId = addCommitDto.ExamId,
                };
                await _examRepository.AddExamToUsersAsync(userExam);
                await _examRepository.SaveAsync();
            }

            commit.UserExam = userExam;
            commit.UserExamId = userExam.Id;
            commit.CommitTime = DateTime.UtcNow;

            await _commitRepository.AddCommitAsync(commit);
            bool response = await _commitRepository.SaveAsync();

            var responseCommit = await _commitRepository.GetCommitAsync(commit.Id);
            return response ? CreatedAtRoute(nameof(GetCommit), new { commitId = commit.Id }, _mapper.Map<CommitDto>(responseCommit)) : Problem();
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

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpPut("{commitId}")]
    [EndpointDescription("更改提交信息")]
    public async Task<IActionResult> UpdateCommit(Guid commitId, UpdateCommitDto updateCommitDto)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            _mapper.Map(updateCommitDto, commit);
            commit.UpdatedAt = DateTime.UtcNow;
            commit.CommitTime = DateTime.UtcNow;
            return await _commitRepository.SaveAsync() ? NoContent() : Problem();
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
    [HttpDelete("{commitId}")]
    [EndpointDescription("删除提交")]
    public async Task<IActionResult> DeleteCommit(Guid commitId)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);
            commit.DeletedAt = DateTime.UtcNow;

            return await _commitRepository.SaveAsync() ? NoContent() : Problem();
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

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpPost("confirmation/{commitId}")]
    [EndpointDescription("确认一个题目中的某个commit有效")]
    public async Task<IActionResult> ConfirmCommit(Guid commitId)
    {
        try
        {
            var confirmationCommit = await _commitRepository.GetCommitAsync(commitId);

            var parameter = new CommitDtoParameter
            {
                UserId = confirmationCommit.UserExam.UserId,
                ExamId = confirmationCommit.UserExam.ExamId,
                ProblemId = confirmationCommit.ProblemId,
            };
            var commits = await _commitRepository.GetCommitsAsync(parameter);

            foreach (var commit in commits)
            {
                if (commit.Id == commitId)
                    continue;
                commit.DeletedAt = DateTime.UtcNow;
            }
            return await _commitRepository.SaveAsync() ? NoContent() : Problem();
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
