using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<User, GroupDto>();
        CreateMap<AddGroupDto, User>();
        CreateMap<UpdateGroupDto, User>();
    }
}
