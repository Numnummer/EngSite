using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class UserText
{
    public string UserLogin { get; set; } = null!;

    public Guid TextId { get; set; }

    public int Id { get; set; }

    public virtual Text Text { get; set; } = null!;

    public virtual User UserLoginNavigation { get; set; } = null!;
}
