using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.Works
{
    public class GetDocumentsRequest
    {
        public string TeacherLogin { get; set; }
        public string StudentLogin { get; set; }
    }
}
