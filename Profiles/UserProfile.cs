using AutoMapper;
using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserWithExamIdsDto>()
            .ForMember(dest => dest.ExamIds, options => options.MapFrom(src => src.Exams));
        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, Guid>()
            .ConvertUsing(user => user.Id);
    }
}
