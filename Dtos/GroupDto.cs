// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Examer.Dtos;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GroupWithUsersDto : GroupDto
{
    public List<UserDto> Users { get; } = [];
}

public class GroupWithUsersAndExamIdsDto : GroupWithUsersDto
{
    public List<Guid> ExamIds { get; } = [];
}

public class AddGroupDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}

public class UpdateGroupDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}
