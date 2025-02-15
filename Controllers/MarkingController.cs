using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Examer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarkingController(ICommitRepository commitRepository, IMapper mapper) : ControllerBase
{
    private readonly ICommitRepository _commitRepository = commitRepository;
    private readonly IMapper _mapper = mapper;

    [HttpPost("{commitId}")]
    public async Task<IActionResult> AddMarking(Guid commitId, [FromQuery, Required] int score)
    {
        try
        {
            var commit = await _commitRepository.GetCommitAsync(commitId);

            commit.Score = score;
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
}
