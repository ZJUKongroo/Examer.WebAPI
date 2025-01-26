using Examer.Database;

namespace Examer.Services;

public class ProblemRepository(ExamerDbContext context) : IProblemRepository
{
    private readonly static string filePath = "../files/"; // This field should be written to the configuration files
    private readonly ExamerDbContext _context = context;

    public async Task UploadFileAsync(Guid examId, int problemId, IFormFile formFile)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));
        
        ArgumentNullException.ThrowIfNull(formFile);

        using var stream = File.Create(GetFilePath(examId, problemId));
        await formFile.CopyToAsync(stream);
    }

    public async Task<MemoryStream> DownloadFileAsync(Guid examId, int problemId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));
        
        var stream = new MemoryStream();

        byte[] fileContent = await File.ReadAllBytesAsync(GetFilePath(examId, problemId));
        await stream.WriteAsync(fileContent);
        stream.Position = 0;

        return stream;
    }

    public void DeleteFile(Guid examId, int problemId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));
        
        if (!FileExists(examId, problemId))
            throw new ArgumentException(nameof(examId));

        File.Delete(GetFilePath(examId, problemId));
    }

    public bool FileExists(Guid examId, int problemId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));
        
        return File.Exists(GetFilePath(examId, problemId));
    }

    private static string GetFilePath(Guid examId, int problemId)
    {
        return filePath + examId.ToString() + "-" + problemId.ToString() + ".pdf";
    }
}
