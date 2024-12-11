using EngSite.Api.Models.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IWorkDoumentRepository
    {
        Task<GetDocumentsResponse[]?> GetDocumentsDataAsync(string teacherLogin, string studentLogin);
        Task<string?> GetDocumentUrlAsync(string documentId, string documentName);
        Task<string?> GetDocumentStatusAsync(string documentId, string documentName);
    }
}
