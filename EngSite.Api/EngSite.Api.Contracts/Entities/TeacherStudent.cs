using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class TeacherStudent
{
    public int Id { get; set; }

    public string Teacherlogin { get; set; } = null!;

    public string Studentlogin { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual User StudentloginNavigation { get; set; } = null!;

    public virtual User TeacherloginNavigation { get; set; } = null!;
}
