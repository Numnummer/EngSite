using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork
{
    public interface IWorksDocumentUnitOfWork
    {
        Task<bool> AddDocumentAsync(byte[] documentBlob,
            string documentMegaId,
            string documentName,
            string teacherLogin,
            string studentLogin);
        Task<bool> SaveDocumentAsync(byte[] documentBlob, string documentMegaId,
            string documentName, string documentStatus);
        Task<bool> SendDocumentAsync(byte[] documentBlob, string documentMegaId,
            string documentName, string documentStatus);
        Task<bool> SendDocumentForRevisionAsync(byte[] documentBlob, string documentMegaId,
            string documentName, string documentStatus);
        Task<bool> DeleteDocumentAsync(string documentMegaId, string documentName);
    }
}
