using AutoMapper;
using Examer.Dtos;
using Examer.Services;
using Examer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExamController(IExamRepository examRepository, IMapper mapper) : ControllerBase
{
    private readonly IExamRepository _examRepository = examRepository;
    private readonly IMapper _mapper = mapper;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExamDto>>> GetExamsAsync()
    {
        var exams = await _examRepository.GetExamsAsync();

        return Ok(_mapper.Map<IEnumerable<ExamDto>>(exams));
    }

    [HttpGet("{examId}")]
    public async Task<ActionResult<ExamDto>> GetExamAsync(Guid examId)
    {
        try
        {
            var exam = await _examRepository.GetExamAsync(examId);

            return Ok(_mapper.Map<ExamDto>(exam));
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
    public async Task<IActionResult> AddExamAsync(AddExamDto addExamDto)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(addExamDto);

            var addExam = _mapper.Map<Exam>(addExamDto);
            addExam.Id = Guid.NewGuid();
            addExam.CreateTime = DateTime.Now;
            addExam.ModifyTime = DateTime.Now;

            await _examRepository.AddExamAsync(addExam);

            return await _examRepository.SaveAsync() ? NoContent() : BadRequest();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpPost("{examId}")]
    public async Task<IActionResult> AddExamToUsersAsync([FromRoute] Guid examId, IEnumerable<Guid> userIds)
    {
        try
        {
            if (examId == Guid.Empty)
                throw new ArgumentNullException(nameof(examId));
            
            await _examRepository.AddExamToUsersAsync(examId, userIds);
            await _examRepository.SaveAsync();

            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }
}
