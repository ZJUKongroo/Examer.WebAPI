// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

using Examer.Enums;

namespace Examer.Dtos;

public class ExamerFileDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public FileType FileType { get; set; }
}

public class AddExamerFileDto
{
    [Required]
    public Guid ParentId { get; set; }

    [Required]
    public FileType FileType { get; set; }
}
