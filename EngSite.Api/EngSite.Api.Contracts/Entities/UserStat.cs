using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class UserStat
{
    public int? WordsLearned { get; set; }

    public int? GrammarProgression { get; set; }

    public int? TextsRead { get; set; }

    public int? AudioListened { get; set; }

    public int? VideoWatched { get; set; }

    public string UserLogin { get; set; } = null!;

    public Guid Id { get; set; }

    public virtual User UserLoginNavigation { get; set; } = null!;
}
