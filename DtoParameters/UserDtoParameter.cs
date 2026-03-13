// Copyright (c) ZJUKongroo. All Rights Reserved.

namespace Examer.DtoParameters;

public class UserDtoParameter : DtoParameterBase
{
    // Filtering fields
    public string? Name { get; set; }
    public string? StudentNumber { get; set; }
    public string? Description { get; set; }
}