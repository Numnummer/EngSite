using System;
using System.Collections.Generic;

namespace EngSite.Api.Models.Entities;

public partial class User
{
    public string FullName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<TeacherStudent> TeacherStudentStudentloginNavigations { get; set; } = new List<TeacherStudent>();

    public virtual ICollection<TeacherStudent> TeacherStudentTeacherloginNavigations { get; set; } = new List<TeacherStudent>();

    public virtual ICollection<UserDictionary> UserDictionaries { get; set; } = new List<UserDictionary>();

    public virtual ICollection<UserStat> UserStats { get; set; } = new List<UserStat>();

    public virtual ICollection<UserText> UserTexts { get; set; } = new List<UserText>();
}
