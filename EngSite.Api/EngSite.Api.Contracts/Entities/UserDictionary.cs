using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class UserDictionary
{
    public string UserLogin { get; set; } = null!;

    public Guid SentenceId { get; set; }

    public int Id { get; set; }

    public virtual Dictionary Sentence { get; set; } = null!;

    public virtual User UserLoginNavigation { get; set; } = null!;
}
