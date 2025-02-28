using Examer.Database;
using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Examer.Services;

public class FileRepository(ExamerDbContext context, IConfiguration configuration) : IFileRepository
{
    private readonly ExamerDbContext _context = context;
    private readonly string _filePathPrefix = configuration["FilePathPrefix"]!;

    public async Task<PagedList<ExamerFile>> GetExamerFilesAsync(ExamerFileDtoParameter parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var queryExpression = _context.Files!
            .OrderBy(x => x.UpdateTime) as IQueryable<ExamerFile>;

        queryExpression = queryExpression.Filtering(parameter);

        return await PagedList<ExamerFile>.CreateAsync(queryExpression!, parameter.PageNumber, parameter.PageSize);
    }

    public async Task<ExamerFile> GetExamerFileAsync(Guid examerFileId)
    {
        if (examerFileId == Guid.Empty)
            throw new ArgumentNullException(nameof(examerFileId));

        var examerFile = await _context.Files!
            .Where(x => x.Id == examerFileId)
            .FirstOrDefaultAsync() ?? throw new NullReferenceException(nameof(examerFileId));
        
        return examerFile;
    }

    public async Task AddExamerFileAsync(ExamerFile examerFile)
    {
        ArgumentNullException.ThrowIfNull(examerFile);

        await _context.AddAsync(examerFile);
    }

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<MemoryStream> GetBlobFileAsync(Guid examerFileId)
    {
        if (examerFileId == Guid.Empty)
            throw new ArgumentNullException(nameof(examerFileId));

        var stream = new MemoryStream();
        byte[] fileContent = await File.ReadAllBytesAsync(await GetBlobFilePath(examerFileId));
        await stream.WriteAsync(fileContent);
        stream.Position = 0;

        return stream;
    }

    public async Task AddBlobFileAsync(Guid examerFileId, IFormFile formFile)
    {
        if (examerFileId == Guid.Empty)
            throw new ArgumentNullException(nameof(examerFileId));
        ArgumentNullException.ThrowIfNull(formFile);

        using var stream = File.Create(await GetBlobFilePath(examerFileId));
        await formFile.CopyToAsync(stream);
    }

    public async Task DeleteBlobFileAsync(Guid examerFileId)
    {
        if (examerFileId == Guid.Empty)
            throw new ArgumentNullException(nameof(examerFileId));

        File.Delete(await GetBlobFilePath(examerFileId));
    }

    public async Task<string> GetBlobFileExtension(Guid examerFileId)
    {
        if (examerFileId == Guid.Empty)
            throw new ArgumentNullException(nameof(examerFileId));

        var examerFile = await GetExamerFileAsync(examerFileId);
        return examerFile.FileName!.Split(".")[^1].ToLower();
    }

    private async Task<string> GetBlobFilePath(Guid examerFileId)
    {
        if (examerFileId == Guid.Empty)
            throw new ArgumentNullException(nameof(examerFileId));

        return Path.GetFullPath($"{_filePathPrefix}/{examerFileId}.{await GetBlobFileExtension(examerFileId)}");
    }

    public static MediaTypeHeaderValue GetBlobFileMimeType(string extension) => extension switch
    {
        "txt" => new MediaTypeHeaderValue("text/plain"),
        "rtf" => new MediaTypeHeaderValue("application/rtf"),
        "csv" => new MediaTypeHeaderValue("text/csv"),
        "bmp" => new MediaTypeHeaderValue("image/bmp"),
        "jpg" => new MediaTypeHeaderValue("image/jpeg"),
        "jpeg" => new MediaTypeHeaderValue("image/jpeg"),
        "png" => new MediaTypeHeaderValue("image/png"),
        "gif" => new MediaTypeHeaderValue("image/gif"),
        "webp" => new MediaTypeHeaderValue("image/webp"),
        "tif" => new MediaTypeHeaderValue("image/tiff"),
        "tiff" => new MediaTypeHeaderValue("image/tiff"),
        "svg" => new MediaTypeHeaderValue("image/svg+xml"),
        "mp3" => new MediaTypeHeaderValue("audio/mpeg"),
        "aac" => new MediaTypeHeaderValue("audio/aac"),
        "mav" => new MediaTypeHeaderValue("audio/wav"),
        "mp4" => new MediaTypeHeaderValue("video/mp4"),
        "webm" => new MediaTypeHeaderValue("video/webm"),
        "avi" => new MediaTypeHeaderValue("video/x-msvideo"),
        "doc" => new MediaTypeHeaderValue("application/msword"),
        "docx" => new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
        "xls" => new MediaTypeHeaderValue("application/vnd.ms-excel"),
        "xlsx" => new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
        "ppt" => new MediaTypeHeaderValue("application/vnd.ms-powerpoint"),
        "pptx" => new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.presentationml.presentation"),
        "pdf" => new MediaTypeHeaderValue("application/pdf"),
        "zip" => new MediaTypeHeaderValue("application/zip"),
        "7z" => new MediaTypeHeaderValue("application/x-7z-compressed"),
        _ => new MediaTypeHeaderValue("application/octet-stream")
    };
}
