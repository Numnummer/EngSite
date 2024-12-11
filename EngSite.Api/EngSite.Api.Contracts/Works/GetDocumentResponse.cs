using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.Works
{
    public class GetDocumentResponse
    {
        public byte[] Blob { get; set; }
        public string Status { get; set; }
    }
}
