using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Examer.Services;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class ProblemController(IProblemRepository problemRepository, IExamRepository examRepository, IMapper mapper) : ControllerBase
{
    private readonly IProblemRepository _problemRepository = problemRepository;
    private readonly IExamRepository _examRepository = examRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost("{examId}")]
    [EndpointDescription("Administrator权限 请注意：此控制器下所有接口可能发生重构，请先不要使用")]
    public async Task<IActionResult> UploadProblem(Guid examId, [FromQuery, Required] int problemId, IFormFile formFile)
    {
        try
        {
            if (examId == Guid.Empty)
                throw new ArgumentNullException(nameof(examId));

            await _problemRepository.UploadFileAsync(examId, problemId, formFile);
            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpGet("{examId}")]
    [HttpHead("{examId}")]
    [EndpointDescription("请注意：此控制器下所有接口可能发生重构，请先不要使用")]
    public async Task<IActionResult> DownloadProblem(Guid examId, [FromQuery, Required] int problemId)
    {
        try
        {
            if (examId == Guid.Empty)
                throw new ArgumentNullException(nameof(examId));

            var exam = await _examRepository.GetExamAsync(examId) ?? throw new ArgumentNullException(nameof(examId));

            // if (problemId < 1 && problemId > exam.ProblemsNumber)
            //     throw new ArgumentOutOfRangeException(nameof(problemId));

            var stream = await _problemRepository.DownloadFileAsync(examId, problemId);

            return new FileStreamResult(stream, "application/pdf");
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
        catch (ArgumentOutOfRangeException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{examId}")]
    [EndpointDescription("请注意：此控制器下所有接口可能发生重构，请先不要使用")]
    public IActionResult DeleteProblem(Guid examId, [FromQuery, Required] int problemId)
    {
        try
        {
            if (examId == Guid.Empty)
                throw new ArgumentNullException(nameof(examId));

            _problemRepository.DeleteFile(examId, problemId);

            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }
}
