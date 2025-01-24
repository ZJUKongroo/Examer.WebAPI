using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
