using AutoMapper;
using Examer.Models;
using Examer.Dtos;

namespace Examer.Profiles;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<Exam, ExamDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<Exam, ExamWithUserOrGroupsDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToLocalTime()))
            .ForMember(dest => dest.UserOrGroupIds, options => options.MapFrom(src => src.Users));
        CreateMap<AddExamDto, Exam>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<UpdateExamDto, ExamDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<Exam, Guid>()
            .ConvertUsing(exam => exam.Id);
    }
}
