using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.User.GetUserData
{
    public class UserProfileData
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public byte[]? Photo { get; set; }
        public string? Role { get; set; }
    }
}
