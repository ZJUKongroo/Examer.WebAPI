using AutoMapper;
using Examer.DtoParameters;
using Examer.Dtos;
using Examer.Helpers;
using Examer.Models;
using Examer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarkingController(IMarkingRepository markingRepository, IMapper mapper) : ControllerBase
{
    private readonly IMarkingRepository _markingRepository = markingRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet(Name = nameof(GetMarkings))]
    [EndpointDescription("获取所有评卷记录  此控制器下均为Administrator权限")]
    public async Task<ActionResult<IEnumerable<Marking>>> GetMarkings(MarkingDtoParameter parameter)
    {
        try
        {
            var markings = await _markingRepository.GetMarkingsAsync(parameter);

            Response.Headers.AppendPaginationHeader(markings, parameter, Url, nameof(GetMarkings));

            var markingDtos = _mapper.Map<IEnumerable<MarkingDto>>(markings);
            return Ok(markingDtos);
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpGet("{markingId}", Name = nameof(GetMarking))]
    [EndpointDescription("获取评卷记录")]
    public async Task<ActionResult<Marking>> GetMarking(Guid markingId)
    {
        try
        {
            var marking = await _markingRepository.GetMarkingAsync(markingId);

            return Ok(_mapper.Map<MarkingDto>(marking));
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
    [EndpointDescription("添加评卷记录")]
    public async Task<IActionResult> AddMarking(AddMarkingDto addMarkingDto)
    {
        try
        {
            var marking = _mapper.Map<Marking>(addMarkingDto);

            marking.Id = Guid.NewGuid();
            marking.CreateTime = DateTime.Now;
            marking.UpdateTime = DateTime.Now;

            await _markingRepository.AddMarkingAsync(marking);

            return await _markingRepository.SaveAsync() ? CreatedAtRoute(nameof(GetMarking), new { markingId = marking.Id }, _mapper.Map<MarkingDto>(marking)) : Problem();
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

    [HttpPut("{markingId}")]
    [EndpointDescription("更新评卷记录")]
    public async Task<IActionResult> UpdateMarking(Guid markingId, UpdateMarkingDto updateMarkingDto)
    {
        try
        {
            var marking = await _markingRepository.GetMarkingAsync(markingId);
            
            _mapper.Map(updateMarkingDto, marking);
            marking.UpdateTime = DateTime.Now;

            return await _markingRepository.SaveAsync() ? NoContent() : Problem();
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

    [HttpDelete("{markingId}")]
    [EndpointDescription("删除评卷记录")]
    public async Task<IActionResult> DeleteMarking(Guid markingId)
    {
        try
        {
            var marking = await _markingRepository.GetMarkingAsync(markingId);

            marking.DeleteTime = DateTime.Now;
            marking.IsDeleted = true;
            
            return await _markingRepository.SaveAsync() ? NoContent() : Problem();
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
