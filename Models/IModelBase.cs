// Copyright (c) ZJUKongroo. All Rights Reserved.

namespace Examer.Models;

public interface IModelBase
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
