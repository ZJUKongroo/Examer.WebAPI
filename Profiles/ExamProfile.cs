using AutoMapper;
using Examer.Models;
using Examer.Dtos;

namespace Examer.Profiles;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<Exam, ExamDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => DateTime.SpecifyKind(src.StartTime, DateTimeKind.Local)))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => DateTime.SpecifyKind(src.EndTime, DateTimeKind.Local)));
        CreateMap<Exam, ExamWithUsersDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => DateTime.SpecifyKind(src.StartTime, DateTimeKind.Local)))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => DateTime.SpecifyKind(src.EndTime, DateTimeKind.Local)));
        CreateMap<AddExamDto, Exam>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<UpdateExamDto, Exam>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<Exam, Guid>()
            .ConvertUsing(exam => exam.Id);
    }
}
