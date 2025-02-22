using AutoMapper;
using Examer.Dtos;
using Examer.Models;
using Examer.Enums;

namespace Examer.Profiles;

public class ExamerFileProfile : Profile
{
    public ExamerFileProfile()
    {
        CreateMap<ExamerFile, ExamerFileDto>();
        CreateMap<AddExamerFileDto, ExamerFile>();
        CreateMap<UpdateExamerFileDto, ExamerFile>();
    }
}
