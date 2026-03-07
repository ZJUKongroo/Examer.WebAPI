// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class AddExamerFileDto
{
    public Guid ParentId { get; set; }
    public FileType FileType { get; set; }
}
