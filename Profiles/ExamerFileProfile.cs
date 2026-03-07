// Copyright (c) ZJUKongroo. All Rights Reserved.

using AutoMapper;

using Examer.Dtos;
using Examer.Enums;
using Examer.Models;

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
