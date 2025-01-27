using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Services;

public interface ICommitRepository
{
    Task<PagedList<Commit>> GetCommitsAsync(CommitDtoParameter parameter);
    Task<Commit> GetCommitAsync(Guid commitId);
    Task<bool> SaveAsync();
}
