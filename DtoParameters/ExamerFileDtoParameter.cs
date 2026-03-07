// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Enums;

namespace Examer.DtoParameters;

public class ExamerFileDtoParameter : DtoParameterBase
{
    public string? FileName { get; set; }
    public FileType FileType { get; set; }
}
