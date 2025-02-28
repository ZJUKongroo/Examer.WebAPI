using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class CommitProfile : Profile
{
    public CommitProfile()
    {
        CreateMap<Commit, CommitWithMarkingsDto>()
            .ForMember(dest => dest.User, options => options.MapFrom(src => src.UserExam.User))
            .ForMember(dest => dest.Exam, options => options.MapFrom(src => src.UserExam.Exam))
            .ForMember(dest => dest.CommitTime, options => options.MapFrom(src => DateTime.SpecifyKind(src.CommitTime, DateTimeKind.Local)));
        CreateMap<Commit, CommitDto>()
            .ForMember(dest => dest.User, options => options.MapFrom(src => src.UserExam.User))
            .ForMember(dest => dest.Exam, options => options.MapFrom(src => src.UserExam.Exam))
            .ForMember(dest => dest.CommitTime, options => options.MapFrom(src => DateTime.SpecifyKind(src.CommitTime, DateTimeKind.Local)));
        CreateMap<AddCommitDto, Commit>();
        CreateMap<UpdateCommitDto, Commit>();
    }
}
