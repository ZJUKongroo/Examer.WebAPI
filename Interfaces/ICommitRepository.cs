// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.DtoParameters;
using Examer.Helpers;
using Examer.Models;

namespace Examer.Interfaces;

public interface ICommitRepository
{
    Task<PagedList<Commit>> GetCommitsAsync(CommitDtoParameter parameter);
    Task<Commit> GetCommitAsync(Guid commitId);
    Task AddCommitAsync(Commit commit);
    Task<bool> CommitExistsAsync(Guid commitId);
    Task<bool> SaveAsync();
}
