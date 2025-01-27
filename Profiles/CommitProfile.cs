using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class CommitProfile : Profile
{
    public CommitProfile()
    {
        CreateMap<Commit, CommitDto>()
            .ForMember(dest => dest.User, options => options.MapFrom(src => src.UserExam.User))
            .ForMember(dest => dest.Exam, options => options.MapFrom(src => src.UserExam.Exam))
            .ForMember(dest => dest.Problem, options => options.MapFrom(src => src.Problem));
        CreateMap<UpdateCommitDto, Commit>();
    }
}
