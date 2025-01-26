using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<User, GroupDto>()
            .ForMember(dest => dest.Users, options => options.MapFrom(src => src.UsersOfGroup));
        CreateMap<AddGroupDto, User>();
        CreateMap<UpdateGroupDto, User>();
    }
}
