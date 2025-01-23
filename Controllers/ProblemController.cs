using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Examer.Services;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Examer.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProblemController(IProblemRepository problemRepository, IExamRepository examRepository, IMapper mapper) : ControllerBase
{
    private readonly IProblemRepository _problemRepository = problemRepository;
    private readonly IExamRepository _examRepository = examRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost("{examId}")]
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
