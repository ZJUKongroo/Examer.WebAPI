using AutoMapper;
using Examer.Models;
using Examer.Dtos;

namespace Examer.Profiles;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<Exam, ExamDto>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<AddExamDto, Exam>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToLocalTime()));
        CreateMap<UpdateExamDto, ExamDto>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime.ToLocalTime()))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime.ToLocalTime()));
    }
}
