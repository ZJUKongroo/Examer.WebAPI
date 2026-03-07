// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<User, GroupWithUsersDto>()
            .ForMember(dest => dest.Users, options => options.MapFrom(src => src.UsersOfGroup));
        CreateMap<User, GroupDto>();
        CreateMap<User, GroupWithUsersAndExamIdsDto>()
            .ForMember(dest => dest.Users, options => options.MapFrom(src => src.UsersOfGroup))
            .ForMember(dest => dest.ExamIds, options => options.MapFrom(src => src.Exams));
        CreateMap<AddGroupDto, User>();
        CreateMap<UpdateGroupDto, User>();
    }
}
