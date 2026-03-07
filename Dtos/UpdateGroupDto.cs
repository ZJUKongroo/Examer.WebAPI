// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class UpdateGroupDto
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
}
