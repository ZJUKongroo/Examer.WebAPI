using AutoMapper;
using Examer.Dtos;
using Examer.Models;
using Examer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class ProblemController(IProblemRepository problemRepository, IExamRepository examRepository, IMapper mapper) : ControllerBase
{
    private readonly IProblemRepository _problemRepository = problemRepository;
    private readonly IExamRepository _examRepository = examRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [EndpointDescription("Administrator权限 向一场考试添加题目")]
    public async Task<IActionResult> AddProblem(AddProblemDto addProblemDto)
    {
        try
        {
            var problem = _mapper.Map<Problem>(addProblemDto);
            problem.Id = Guid.NewGuid();
            problem.CreateTime = DateTime.Now;
            problem.UpdateTime = DateTime.Now;

            await _problemRepository.AddProblemAsync(problem);
            return await _problemRepository.SaveAsync() ? CreatedAtRoute(nameof(GetProblem), new { problemId = problem.Id }, _mapper.Map<ProblemDto>(problem)) : Problem();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpPost("file/{problemId}")]
    [EndpointDescription("上传题目文件")]
    public async Task<IActionResult> AddProblemFile(Guid problemId, IFormFile formFile)
    {
        try
        {
            var problem = await _problemRepository.GetProblemAsync(problemId);

            await _problemRepository.AddProblemFileAsync(problem, formFile);
            return CreatedAtRoute(nameof(GetProblemFile), new { problemId }, null);
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

    [HttpGet("{problemId}", Name = nameof(GetProblem))]
    [EndpointDescription("获取题目信息")]
    public async Task<ActionResult<ProblemDto>> GetProblem(Guid problemId)
    {
        try
        {
            var problem = await _problemRepository.GetProblemAsync(problemId);
            
            var problemDto = _mapper.Map<ProblemDto>(problem);
            return Ok(problemDto);
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

    [HttpGet("file/{problemId}", Name = nameof(GetProblemFile))]
    [HttpHead("file/{problemId}", Name = nameof(GetProblemFile))]
    [EndpointDescription("获取题目文件")]
    public async Task<IActionResult> GetProblemFile(Guid problemId)
    {
        try
        {            
            var stream = await _problemRepository.GetProblemFileAsync(problemId);

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

    [HttpPut("{problemId}")]
    [EndpointDescription("更新题目")]
    public async Task<IActionResult> UpdateProblem(Guid problemId, UpdateProblemDto updateProblemDto)
    {
        try
        {
            var problem = await _problemRepository.GetProblemAsync(problemId);

            _mapper.Map(updateProblemDto, problem);
            problem.UpdateTime = DateTime.Now;
            return await _problemRepository.SaveAsync() ? NoContent() : Problem();
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

    [HttpDelete("{problemId}")]
    [EndpointDescription("删除题目和文件")]
    public async Task<IActionResult> DeleteProblem(Guid problemId)
    {
        try
        {
            var problem = await _problemRepository.GetProblemAsync(problemId);
            problem.DeleteTime = DateTime.Now;
            problem.IsDeleted = true;

            _problemRepository.DeleteProblemFile(problem);

            return await _problemRepository.SaveAsync() ? NoContent() : Problem();
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

    [HttpDelete("file/{problemId}")]
    [EndpointDescription("删除题目文件")]
    public async Task<IActionResult> DeleteProblemFile(Guid problemId)
    {
        try
        {
            var problem = await _problemRepository.GetProblemAsync(problemId);

            _problemRepository.DeleteProblemFile(problem);
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
