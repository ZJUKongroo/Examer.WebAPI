using AutoMapper;
using Examer.Dtos;
using Examer.Services;
using Examer.Models;
using Microsoft.AspNetCore.Mvc;
using Examer.DtoParameters;
using Microsoft.AspNetCore.Authorization;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class ExamController(IExamRepository examRepository, IUserRepository userRepository, IMapper mapper) : ControllerBase
{
    private readonly IExamRepository _examRepository = examRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IMapper _mapper = mapper;
    
    [HttpGet]
    [EndpointDescription("获取所有考试 此控制器下均为Administrator权限")]
    public async Task<ActionResult<IEnumerable<ExamDto>>> GetExams([FromQuery] ExamDtoParameter parameter)
    {
        var exams = await _examRepository.GetExamsAsync(parameter);

        return Ok(_mapper.Map<IEnumerable<ExamDto>>(exams));
    }

    [HttpGet("{examId}")]
    [EndpointDescription("根据examId获取考试")]
    public async Task<ActionResult<ExamDto>> GetExam(Guid examId)
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
    [EndpointDescription("添加考试")]
    public async Task<IActionResult> AddExam(AddExamDto addExamDto)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(addExamDto);

            var addExam = _mapper.Map<Exam>(addExamDto);

            addExam.Id = Guid.NewGuid();
            addExam.CreateTime = DateTime.Now;
            addExam.UpdateTime = DateTime.Now;
            await _examRepository.AddExamAsync(addExam);

            return await _examRepository.SaveAsync() ? Created() : BadRequest();
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpPut("{examId}")]
    [EndpointDescription("更新考试信息")]
    public async Task<IActionResult> UpdateExam(Guid examId, UpdateExamDto updateExamDto)
    {
        try
        {
            var exam = await _examRepository.GetExamAsync(examId);

            _mapper.Map(updateExamDto, exam);
            exam.UpdateTime = DateTime.Now;
            return await _examRepository.SaveAsync() ? NoContent() : Problem();
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

    [HttpDelete("{examId}")]
    [EndpointDescription("删除考试")]
    public async Task<IActionResult> DeleteExam(Guid examId)
    {
        try
        {
            var exam = await _examRepository.GetExamAsync(examId);

            exam.DeleteTime = DateTime.Now;
            exam.IsDeleted = true;
            return await _examRepository.SaveAsync() ? NoContent() : Problem();
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

    [HttpPost("{examId}")]
    [EndpointDescription("为students分配一场考试")]
    public async Task<IActionResult> AddExamToUsers([FromRoute] Guid examId, IEnumerable<Guid> userIds)
    {
        try
        {
            await _examRepository.GetExamAsync(examId);
            foreach (var userId in userIds)
            {
                if (!await _userRepository.UserExistsAsync(userId))
                    continue;
                
                var userExam = new UserExam
                {
                    UserId = userId,
                    ExamId = examId,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                await _examRepository.AddExamToUsersAsync(userExam);
            }
            return await _examRepository.SaveAsync() ? Created() : Problem();
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
