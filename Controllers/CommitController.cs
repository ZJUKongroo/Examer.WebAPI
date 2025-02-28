using AutoMapper;
using Examer.Dtos;
using Examer.DtoParameters;
using Examer.Services;
using Microsoft.AspNetCore.Mvc;
using Examer.Helpers;
using Examer.Models;
using Microsoft.AspNetCore.Authorization;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
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
            var commits = await _commitRepository.GetCommitsAsync(parameter);

            Response.Headers.AppendPaginationHeader(commits, parameter, Url, nameof(GetCommits));

            var commitDtos = _mapper.Map<IEnumerable<CommitDto>>(commits);
            return Ok(commitDtos);
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [Authorize(Roles = "Administrator, Manager")]
    [HttpGet("{commitId}", Name = nameof(GetCommit))]
    [EndpointDescription("根据commitId获取用户")]
    public async Task<ActionResult<CommitDto>> GetCommit(Guid commitId)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            return Ok(_mapper.Map<CommitDto>(commit));
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

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpPost]
    [EndpointDescription("添加提交信息")]
    public async Task<ActionResult<CommitDto>> AddCommit(AddCommitDto addCommitDto)
    {
        try
        {
            var commit = _mapper.Map<Commit>(addCommitDto);
            var userExam = await _examRepository.GetUserExamAsync(addCommitDto.UserId, addCommitDto.ExamId);
            
            commit.Id = Guid.NewGuid();
            commit.UserExam = userExam;
            commit.UserExamId = userExam.Id;
            commit.CreateTime = DateTime.Now;
            commit.UpdateTime = DateTime.Now;
            commit.CommitTime = DateTime.Now;

            await _commitRepository.AddCommitAsync(commit);
            bool response = await _commitRepository.SaveAsync();

            var responseCommit = await _commitRepository.GetCommitAsync(commit.Id);
            return response ? CreatedAtRoute(nameof(GetCommit), new { commitId = commit.Id }, _mapper.Map<CommitDto>(responseCommit)) : Problem();
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

    [Authorize(Roles = "Administrator, Manager, Student")]
    [HttpPut("{commitId}")]
    [EndpointDescription("更改提交信息")]
    public async Task<IActionResult> UpdateCommit(Guid commitId, UpdateCommitDto updateCommitDto)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            _mapper.Map(updateCommitDto, commit);
            commit.UpdateTime = DateTime.Now;
            commit.CommitTime = DateTime.Now;
            return await _commitRepository.SaveAsync() ? NoContent() : Problem();
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

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{commitId}")]
    [EndpointDescription("删除提交")]
    public async Task<IActionResult> DeleteCommit(Guid commitId)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);
            commit.DeleteTime = DateTime.Now;
            commit.IsDeleted = true;

            return await _commitRepository.SaveAsync() ? NoContent() : Problem();
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
