using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class Forum
{
    public string Message { get; set; } = null!;

    public DateTime Date { get; set; }

    public string AuthorName { get; set; } = null!;

    public Guid Id { get; set; }
}
