using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class ProblemProfile : Profile
{
    public ProblemProfile()
    {
        CreateMap<Problem, ProblemDto>();
        CreateMap<AddProblemDto, Problem>();
        CreateMap<UpdateProblemDto, Problem>();
    }
}
