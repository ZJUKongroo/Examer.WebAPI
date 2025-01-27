using AutoMapper;
using Examer.Dtos;
using Examer.DtoParameters;
using Examer.Services;
using Microsoft.AspNetCore.Mvc;
using Examer.Helpers;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommitController(ICommitRepository commitRepository, IMapper mapper) : ControllerBase
{
    private readonly ICommitRepository _commitRepository = commitRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetCommits))]
    [EndpointDescription("获取所有提交 可任意分页和筛选 此控制器下均为Administrator权限")]
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

    [HttpGet("{commitId}")]
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

    [HttpPut("{commitId}")]
    [EndpointDescription("更改提交信息")]
    public async Task<IActionResult> UpdateCommit(Guid commitId, UpdateCommitDto updateCommitDto)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            _mapper.Map(updateCommitDto, commit);
            commit.UpdateTime = DateTime.Now;
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
