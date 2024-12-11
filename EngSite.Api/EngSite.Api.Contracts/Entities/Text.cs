using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class Text
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public virtual ICollection<UserText> UserTexts { get; set; } = new List<UserText>();
}
