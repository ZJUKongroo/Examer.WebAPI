using AutoMapper;
using Examer.Models;
using Examer.Dtos;

namespace Examer.Profiles;

public class ExamManagerProfile : Profile
{
    public ExamManagerProfile()
    {
        CreateMap<Exam, ExamDto>();
        CreateMap<AddExamDto, Exam>();
    }
}
