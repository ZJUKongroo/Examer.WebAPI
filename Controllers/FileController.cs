using AutoMapper;
using Examer.DtoParameters;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Models;
using Examer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator, Manager, Student")]
public class FileController(IFileRepository fileRepository, IMapper mapper) : ControllerBase
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IMapper _mapper = mapper;

    [Authorize(Roles = "Administrator, Manager")]
    [HttpGet(Name = nameof(GetExamerFiles))]
    [EndpointDescription("获取所有文件信息")]
    public async Task<ActionResult<IEnumerable<ExamerFileDto>>> GetExamerFiles([FromQuery] ExamerFileDtoParameter parameter)
    {
        try
        {
            var examerFiles = await _fileRepository.GetExamerFilesAsync(parameter);

            Response.Headers.AppendPaginationHeader(examerFiles, parameter, Url, nameof(GetExamerFiles));

            var examerFileDtos = _mapper.Map<IEnumerable<ExamerFileDto>>(examerFiles);
            return Ok(examerFileDtos);
        }
        catch (ArgumentNullException)
        {
            return BadRequest();
        }
    }

    [HttpGet("{fileId}", Name = nameof(GetExamerFile))]
    [EndpointDescription("获取文件信息")]
    public async Task<ActionResult> GetExamerFile(Guid fileId)
    {
        try
        {
            var examerFile = await _fileRepository.GetExamerFileAsync(fileId);

            return Ok(_mapper.Map<ExamerFileDto>(examerFile));
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
    [EndpointDescription("添加文件信息")]
    public async Task<ActionResult<ExamerFileDto>> AddExamerFile(AddExamerFileDto addExamerFileDto)
    {
        try
        {
            var examerFile = _mapper.Map<ExamerFile>(addExamerFileDto);

            examerFile.Id = Guid.NewGuid();
            examerFile.CreateTime = DateTime.Now;
            examerFile.UpdateTime = DateTime.Now;

            switch (examerFile.FileType)
            {
                case FileType.CommitFile:
                    examerFile.CommitId = addExamerFileDto.ParentId;
                    break;
                case FileType.ProblemFile:
                    examerFile.ProblemId = addExamerFileDto.ParentId;
                    break;
                default:
                    break;
            }

            await _fileRepository.AddExamerFileAsync(examerFile);

            return await _fileRepository.SaveAsync() ? CreatedAtRoute(nameof(GetExamerFile), new { fileId = examerFile.Id }, _mapper.Map<ExamerFileDto>(examerFile)) : Problem();
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

    [Authorize(Roles = "Administrator")]
    [HttpPut("{fileId}")]
    [EndpointDescription("更新文件信息")]
    public async Task<IActionResult> UpdateExamerFile(Guid fileId, UpdateExamerFileDto updateExamerFileDto)
    {
        try
        {
            var examerFile = await _fileRepository.GetExamerFileAsync(fileId);
            
            _mapper.Map(updateExamerFileDto, examerFile);
            examerFile.UpdateTime = DateTime.Now;

            return await _fileRepository.SaveAsync() ? NoContent() : Problem();
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

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{fileId}")]
    [EndpointDescription("删除文件信息")]
    public async Task<IActionResult> DeleteExamerFile(Guid fileId)
    {
        try
        {
            var examerFile = await _fileRepository.GetExamerFileAsync(fileId);

            examerFile.DeleteTime = DateTime.Now;
            examerFile.IsDeleted = true;

            return await _fileRepository.SaveAsync() ? NoContent() : Problem();
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

    [HttpGet("blob/{fileId}", Name = nameof(GetBlobFile))]
    [HttpHead("blob/{fileId}", Name = nameof(GetBlobFile))]
    [EndpointDescription("获取上传的文件")]
    public async Task<IActionResult> GetBlobFile(Guid fileId)
    {
        try
        {
            var stream = await _fileRepository.GetBlobFileAsync(fileId);
            string extension = await _fileRepository.GetBlobFileExtension(fileId);

            return new FileStreamResult(stream, FileRepository.GetBlobFileMimeType(extension));
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

    [HttpPost("blob/{fileId}")]
    [EndpointDescription("提交一个文件")]
    public async Task<IActionResult> AddBlobFile(Guid fileId, IFormFile formFile)
    {
        try
        {
            var examerFile = await _fileRepository.GetExamerFileAsync(fileId);
            examerFile.FileSize = formFile.Length;
            examerFile.FileName = formFile.FileName;
            await _fileRepository.SaveAsync();

            await _fileRepository.AddBlobFileAsync(fileId, formFile);
            return CreatedAtRoute(nameof(GetBlobFile), new { fileId }, null);
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

    [Authorize(Roles = "Administrator")]
    [HttpDelete("blob/{fileId}")]
    [EndpointDescription("删除上传的文件")]
    public async Task<IActionResult> DeleteBlobFile(Guid fileId)
    {
        try
        {
            await _fileRepository.DeleteBlobFileAsync(fileId);
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
