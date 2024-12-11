using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class Document
{
    public int Id { get; set; }

    public string Url { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int TeacherStudentId { get; set; }

    public Guid Megadocumentid { get; set; }

    public string Name { get; set; } = null!;

    public virtual TeacherStudent TeacherStudent { get; set; } = null!;
}
