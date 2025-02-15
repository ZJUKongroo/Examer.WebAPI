using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Services;

public interface ICommitRepository
{
    Task<PagedList<Commit>> GetCommitsAsync(CommitDtoParameter parameter);
    Task<Commit> GetCommitAsync(Guid commitId);
    Task<MemoryStream> GetCommitFileAsync(Guid commitId);
    Task AddCommitAsync(Commit commit);
    Task AddCommitFileAsync(Commit commit, IFormFile formFile);
    void DeleteCommitFile(Commit commit);
    Task<bool> CommitExistsAsync(Guid commitId);
    Task<bool> SaveAsync();
}
