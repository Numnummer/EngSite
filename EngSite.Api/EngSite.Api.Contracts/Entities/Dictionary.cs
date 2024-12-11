using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class Dictionary
{
    public Guid Id { get; set; }

    public string Sentence { get; set; } = null!;

    public string Translation { get; set; } = null!;

    public virtual ICollection<UserDictionary> UserDictionaries { get; set; } = new List<UserDictionary>();
}
