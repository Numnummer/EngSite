using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.Forum
{
    public class ForumMessage
    {
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
