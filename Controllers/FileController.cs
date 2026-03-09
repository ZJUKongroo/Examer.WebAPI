// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.DtoParameters;
using Examer.Dtos;
using Examer.Enums;
using Examer.Helpers;
using Examer.Interfaces;
using Examer.Models;
using Examer.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Examer.Controllers;

[ApiController]
[Route("api/file")]
[Authorize]
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
        catch (EmptyGuidException)
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
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
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

            switch (examerFile.FileType)
            {
                case FileType.CommitFile:
                    examerFile.CommitId = addExamerFileDto.ParentId;
                    examerFile.ProblemId = null;
                    break;
                case FileType.ProblemFile:
                    examerFile.ProblemId = addExamerFileDto.ParentId;
                    examerFile.CommitId = null;
                    break;
                default:
                    return BadRequest();
            }

            await _fileRepository.AddExamerFileAsync(examerFile);

            return await _fileRepository.SaveAsync() ? CreatedAtRoute(nameof(GetExamerFile), new { fileId = examerFile.Id }, _mapper.Map<ExamerFileDto>(examerFile)) : Problem();
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{fileId}")]
    [EndpointDescription("更新文件信息")]
    public async Task<IActionResult> UpdateExamerFile(Guid fileId)
    {
        try
        {
            var examerFile = await _fileRepository.GetExamerFileAsync(fileId);

            examerFile.UpdatedAt = DateTime.UtcNow;

            return await _fileRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
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

            examerFile.DeletedAt = DateTime.UtcNow;

            return await _fileRepository.SaveAsync() ? NoContent() : Problem();
        }
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
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
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
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
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
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
        catch (EmptyGuidException)
        {
            return BadRequest();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
