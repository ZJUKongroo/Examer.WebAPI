// Copyright (c) ZJUKongroo. All Rights Reserved.

using System.ComponentModel.DataAnnotations.Schema;

namespace Examer.Models;

public class ModelBase : IModelBase
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}
