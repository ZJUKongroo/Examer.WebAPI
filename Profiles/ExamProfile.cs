using AutoMapper;
using Examer.Models;
using Examer.Dtos;

namespace Examer.Profiles;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<Exam, ExamDto>();
        CreateMap<AddExamDto, Exam>();
    }
}
