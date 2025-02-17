using AutoMapper;
using Examer.Dtos;
using Examer.DtoParameters;
using Examer.Services;
using Microsoft.AspNetCore.Mvc;
using Examer.Helpers;
using Examer.Models;

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

    [HttpGet("file/{commitId}", Name = nameof(GetCommitFile))]
    [HttpHead("file/{commitId}", Name = nameof(GetCommitFile))]
    [EndpointDescription("获取上传的答案文件")]
    public async Task<IActionResult> GetCommitFile(Guid commitId)
    {
        try
        {
            var stream = await _commitRepository.GetCommitFileAsync(commitId);

            return new FileStreamResult(stream, "application/pdf");
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
    [EndpointDescription("添加提交信息")]
    public async Task<ActionResult<CommitDto>> AddCommit(AddCommitDto addCommitDto)
    {
        try
        {
            var commit = _mapper.Map<Commit>(addCommitDto);
            commit.Id = Guid.NewGuid();
            commit.CreateTime = DateTime.Now;
            commit.UpdateTime = DateTime.Now;
            commit.CommitTime = DateTime.Now;

            await _commitRepository.AddCommitAsync(commit);
            return await _commitRepository.SaveAsync() ? CreatedAtRoute(nameof(GetCommit), new { commitId = commit.Id }, _mapper.Map<CommitDto>(commit)) : Problem();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpPost("file/{commitId}")]
    [EndpointDescription("提交答案文件")]
    public async Task<IActionResult> AddCommitFile(Guid commitId, IFormFile formFile)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            await _commitRepository.AddCommitFileAsync(commit, formFile);
            return CreatedAtRoute(nameof(GetCommitFile), new { commitId }, null);
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

    [HttpDelete("{commitId}")]
    [EndpointDescription("删除提交和答案文件")]
    public async Task<IActionResult> DeleteCommit(Guid commitId)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);
            commit.DeleteTime = DateTime.Now;
            commit.IsDeleted = true;

            _commitRepository.DeleteCommitFile(commit);

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

    [HttpDelete("file/{commitId}")]
    [EndpointDescription("删除答案文件")]
    public async Task<IActionResult> DeleteCommitFile(Guid commitId)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);
            
            _commitRepository.DeleteCommitFile(commit);
            return NoContent();
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
