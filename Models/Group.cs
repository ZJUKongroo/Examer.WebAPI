// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Examer.Models;

[Table("group")]
[Index(nameof(GroupId), Name = "group_group_id_idx")]
[Index(nameof(UserOfGroupId), Name = "group_user_of_group_id_idx")]
public class Group : ModelBase
{
    [Column("id"), Key]
    public Guid Id { get; set; }

    [Column("group_id"), ForeignKey(nameof(GroupUser))]
    public Guid GroupId { get; set; }
    public User GroupUser { get; set; } = null!;

    [Column("user_of_group_id"), ForeignKey(nameof(User))]
    public Guid UserOfGroupId { get; set; }
    public User User { get; set; } = null!;
}
