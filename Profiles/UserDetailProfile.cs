// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.Dtos;
using Examer.Models;

namespace Examer.Profiles;

public class UserDetailProfile : Profile
{
    public UserDetailProfile()
    {
        CreateMap<UserDetail, UserDetailDto>();
        CreateMap<AddUserDetailDto, UserDetail>();
        CreateMap<UpdateUserDetailDto, UserDetail>();
    }
}
