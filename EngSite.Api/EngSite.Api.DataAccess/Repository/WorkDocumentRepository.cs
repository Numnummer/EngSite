using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.DataAccess.Repository
{
    public class WorkDocumentRepository(EnglishSiteContext dbContext,
        ILogger<WorkDocumentRepository> logger) : IWorkDoumentRepository
    {
        public async Task<GetDocumentsResponse[]?> GetDocumentsDataAsync(string teacherLogin, string studentLogin)
        {
            try
            {
                var id = (await dbContext.TeacherStudents.FirstOrDefaultAsync(e => e.Teacherlogin==teacherLogin&&e.Studentlogin==studentLogin)).Id;
                return await dbContext.Documents.
                    Where(docs => docs.TeacherStudentId==id).
                    Select(docs => new GetDocumentsResponse()
                    {
                        Id=docs.Megadocumentid,
                        Name=docs.Name,
                        Status=docs.Status
                    }).ToArrayAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<string?> GetDocumentStatusAsync(string documentId, string documentName)
        {
            try
            {
                var document = await dbContext.Documents.
                    FirstOrDefaultAsync(doc =>
                        doc.Megadocumentid==Guid.Parse(documentId)
                            &&doc.Name==documentName);
                return document?.Status;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<string?> GetDocumentUrlAsync(string documentId, string documentName)
        {
            try
            {
                var document = await dbContext.Documents.
                    FirstOrDefaultAsync(doc =>
                        doc.Megadocumentid==Guid.Parse(documentId)
                            &&doc.Name==documentName);
                return document?.Url;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
