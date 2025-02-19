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
        try
        {
            var exams = await _examRepository.GetExamsAsync(parameter);

            return Ok(_mapper.Map<IEnumerable<ExamDto>>(exams));
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpGet("{examId}", Name = nameof(GetExam))]
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
    public async Task<ActionResult<ExamDto>> AddExam(AddExamDto addExamDto)
    {
        try
        {
            var addExam = _mapper.Map<Exam>(addExamDto);

            addExam.Id = Guid.NewGuid();
            addExam.CreateTime = DateTime.Now;
            addExam.UpdateTime = DateTime.Now;
            await _examRepository.AddExamAsync(addExam);

            return await _examRepository.SaveAsync() ? CreatedAtRoute(nameof(GetExam), new { examId = addExam.Id }, _mapper.Map<ExamDto>(addExam)) : BadRequest();
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

    [HttpPost("assignment/{examId}")]
    [EndpointDescription("为student或group分配一场考试")]
    public async Task<IActionResult> AddExamToUserOrGroups(Guid examId, IEnumerable<Guid> userOrGroupIds)
    {
        try
        {
            await _examRepository.GetExamAsync(examId);
            foreach (var userOrGroupId in userOrGroupIds)
            {
                if (!await _userRepository.UserOrGroupExistsAsync(userOrGroupId))
                    continue;
                
                var userExam = new UserExam
                {
                    Id = Guid.NewGuid(),
                    UserId = userOrGroupId,
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

    [HttpDelete("assignment/{examId}")]
    [EndpointDescription("把student或group从一场考试中删除")]
    public async Task<IActionResult> DeleteExamToUserOrGroups(Guid examId, IEnumerable<Guid> userOrGroupIds)
    {
        try
        {
            await _examRepository.GetExamAsync(examId);
            foreach (var userOrGroupId in userOrGroupIds)
            {
                if (!await _userRepository.UserOrGroupExistsAsync(userOrGroupId))
                    continue;
                
                var userExam = await _examRepository.GetUserExamAsync(userOrGroupId, examId);
                userExam.DeleteTime = DateTime.Now;
                userExam.IsDeleted = true;
            }
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

    [HttpGet("users/{examId}")]
    [EndpointDescription("获取一个考试的用户")]
    public async Task<ActionResult<ExamWithUsersDto>> GetUsersWithExamId(Guid examId)
    {
        try
        {
            var examWithUsers = await _examRepository.GetExamWithUsersAsync(examId);
            
            return Ok(_mapper.Map<ExamWithUsersDto>(examWithUsers));
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

    [HttpGet("groups/{examId}")]
    [EndpointDescription("获取一个考试的组")]
    public async Task<ActionResult<ExamWithUsersDto>> GetGroupsWithExamId(Guid examId)
    {
        try
        {
            var examWithUsers = await _examRepository.GetExamWithGroupsAsync(examId);
            
            return Ok(_mapper.Map<ExamWithUsersDto>(examWithUsers));
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

    [HttpGet("userOrGroups/{examId}")]
    [EndpointDescription("获取一个考试的用户和组")]
    public async Task<ActionResult<ExamWithUsersDto>> GetUserOrGroupsWithExamId(Guid examId)
    {
        try
        {
            var examWithUsers = await _examRepository.GetExamWithUserOrGroupsAsync(examId);

            return Ok(_mapper.Map<ExamWithUsersDto>(examWithUsers));
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

    [HttpPost("inheritance/{examId}")]
    [EndpointDescription("向一场考试添加继承关系（父考试）")]
    public async Task<IActionResult> AddExamInheritance(Guid examId, IEnumerable<Guid> inheritedExamIds)
    {
        try
        {
            await _examRepository.GetExamAsync(examId);
            foreach (var inheritedExamId in inheritedExamIds)
            {
                if (!await _examRepository.ExamExistsAsync(inheritedExamId))
                    continue;

                var examInheritance = new ExamInheritance
                {
                    Id = Guid.NewGuid(),
                    InheritedExamId = inheritedExamId,
                    InheritingExamId = examId,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };
                await _examRepository.AddExamInheritanceAsync(examInheritance);
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

    [HttpDelete("inheritance/{examId}")]
    [EndpointDescription("删除一场考试的继承关系（父考试）")]
    public async Task<IActionResult> DeleteExamInheritance(Guid examId, IEnumerable<Guid> inheritedExamIds)
    {
        try
        {
            await _examRepository.GetExamAsync(examId);
            foreach (var inheritedExamId in inheritedExamIds)
            {
                if (!await _examRepository.ExamExistsAsync(inheritedExamId))
                    continue;
                
                var examInheritance = await _examRepository.GetExamInheritanceAsync(inheritedExamId, examId);
                examInheritance.DeleteTime = DateTime.Now;
                examInheritance.IsDeleted = true;
            }
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
}
