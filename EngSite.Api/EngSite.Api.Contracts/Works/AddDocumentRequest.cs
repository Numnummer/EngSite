using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.Works
{
    public class AddDocumentRequest
    {
        public string? DocumentBase64 { get; set; }
        public string? DocumentName { get; set; }
        public string? StudentLogin { get; set; }
        public string? DocumentId { get; set; }
    }
}
