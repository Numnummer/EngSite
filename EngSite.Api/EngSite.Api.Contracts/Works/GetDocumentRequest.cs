using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.Works
{
    public class GetDocumentRequest
    {
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
    }
}
