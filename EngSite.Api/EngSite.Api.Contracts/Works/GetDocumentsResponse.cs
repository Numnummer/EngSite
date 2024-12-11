using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.Models.Works
{
    public class GetDocumentsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
