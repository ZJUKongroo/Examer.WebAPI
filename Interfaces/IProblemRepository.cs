// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Models;

namespace Examer.Interfaces;

public interface IProblemRepository
{
    Task AddProblemAsync(Problem problem);
    Task<Problem> GetProblemAsync(Guid problemId);
    Task<bool> ProblemExistsAsync(Guid problemId);
    Task<bool> SaveAsync();
}
