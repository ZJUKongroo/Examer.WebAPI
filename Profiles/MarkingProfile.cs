using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class MarkingProfile : Profile
{
    public MarkingProfile()
    {
        CreateMap<Marking, MarkingDto>();
        CreateMap<AddMarkingDto, Marking>();
        CreateMap<UpdateMarkingDto, Marking>();
    }
}
