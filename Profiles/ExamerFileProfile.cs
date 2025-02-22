using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class ExamerFileProfile : Profile
{
    ExamerFileProfile()
    {
        CreateMap<ExamerFile, ExamerFileDto>();
    }
}
