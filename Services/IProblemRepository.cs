namespace Examer.Services;

public interface IProblemRepository
{
    Task UploadFileAsync(Guid examId, int problemId, IFormFile formFile);
    Task<MemoryStream> DownloadFileAsync(Guid examId, int problemId);
    void DeleteFile(Guid examId, int problemId);
    bool FileExists(Guid examId, int problemId);
}
