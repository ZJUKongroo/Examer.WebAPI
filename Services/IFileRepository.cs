using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;
using Microsoft.Net.Http.Headers;

namespace Examer.Services;

public interface IFileRepository
{
    Task<PagedList<ExamerFile>> GetExamerFilesAsync(ExamerFileDtoParameter parameter);
    Task<ExamerFile> GetExamerFileAsync(Guid examerFileId);
    Task AddExamerFileAsync(ExamerFile examerFile);
    Task<bool> SaveAsync();
    Task<MemoryStream> GetBlobFileAsync(Guid examerFileId);
    Task AddBlobFileAsync(Guid examerFileId, IFormFile formFile);
    Task DeleteBlobFileAsync(Guid examerFileId);
    Task<string> GetBlobFileExtension(Guid examerFileId);
}
