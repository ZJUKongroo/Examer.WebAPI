// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<Exam, ExamDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToUniversalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToUniversalTime()));
        CreateMap<Exam, ExamWithUsersDto>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToUniversalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToUniversalTime()));
        CreateMap<AddExamDto, Exam>()
            .ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime.ToUniversalTime()))
            .ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime.ToUniversalTime()));
        var updateMap = CreateMap<UpdateExamDto, Exam>();
        updateMap.ForAllMembers(options => options.Condition((src, _, srcMember) => srcMember != null));
        updateMap.ForMember(dest => dest.StartTime, options => options.MapFrom(src => src.StartTime!.Value.ToUniversalTime()));
        updateMap.ForMember(dest => dest.EndTime, options => options.MapFrom(src => src.EndTime!.Value.ToUniversalTime()));
        CreateMap<Exam, Guid>()
            .ConvertUsing(exam => exam.Id);
    }
}
