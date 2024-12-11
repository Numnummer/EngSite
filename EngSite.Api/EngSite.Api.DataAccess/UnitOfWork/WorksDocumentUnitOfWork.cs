using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork;
using EngSite.Api.Models.Entities;
using EngSite.Api.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EngSite.Api.DataAccess.UnitOfWork
{
    public class WorksDocumentUnitOfWork(EnglishSiteContext dbContext,
        IMegaRepository megaRepository, IConfiguration configuration,
        ILogger<WorksDocumentUnitOfWork> logger) : IWorksDocumentUnitOfWork
    {
        public async Task<bool> AddDocumentAsync(byte[] documentBlob,
            string documentMegaId,
            string documentName,
            string teacherLogin,
            string studentLogin)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            var documentData = new UploadedDocumentData();
            try
            {
                var name = $"{documentMegaId} {documentName}";
                if (await megaRepository.IsDocumentExistsAsync(name))
                {
                    await megaRepository.DeleteDocumentByNameAsync(name);
                    var foundedDocument = await dbContext.Documents.FirstOrDefaultAsync(doc => doc.Megadocumentid==Guid.Parse(documentMegaId));
                    dbContext.Documents.Remove(foundedDocument);
                    await dbContext.SaveChangesAsync();
                }

                documentData = await megaRepository.UploadDocumentAsync(documentBlob, documentName)??throw new Exception();
                var documentId = (await dbContext.TeacherStudents.FirstOrDefaultAsync(e => e.Teacherlogin==teacherLogin && e.Studentlogin==studentLogin)).Id;

                var document = new Document()
                {
                    Status=configuration["WorksDocumentStatus:Created"],
                    Url = documentData.Link,
                    TeacherStudentId = documentId,
                    Megadocumentid=documentData.Id,
                    Name = documentName,
                };
                await dbContext.Documents.AddAsync(document);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                if (documentData.Link!= null)
                {
                    await megaRepository.DeleteDocumentAsync(documentData.Link);
                }
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteDocumentAsync(string documentMegaId, string documentName)
        {
            try
            {
                var name = $"{documentMegaId} ${documentName}";
                await megaRepository.DeleteDocumentByNameAsync(name);
                var foundedDocument = await dbContext.Documents.FirstOrDefaultAsync(doc => doc.Megadocumentid==Guid.Parse(documentMegaId));
                dbContext.Documents.Remove(foundedDocument);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> SaveDocumentAsync(byte[] documentBlob, string documentMegaId, string documentName, string documentStatus)
        {
            try
            {
                var name = $"{documentMegaId} {documentName}";
                await megaRepository.DeleteDocumentByNameAsync(name);
                var documentData = await megaRepository.UploadDocumentAsync(documentBlob, documentName)??throw new Exception();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<bool> SendDocumentAsync(byte[] documentBlob, string documentMegaId, string documentName, string documentStatus)
        {
            try
            {
                var name = $"{documentMegaId} {documentName}";
                await megaRepository.DeleteDocumentByNameAsync(name);
                var documentData = await megaRepository.UploadDocumentAsync(documentBlob, documentName)??throw new Exception();
                var newStatus = "None";

                switch (documentStatus)
                {
                    case "Created":
                        newStatus="Sent to student";
                        break;
                    case "Sent to student":
                        newStatus="Waiting for check";
                        break;
                    case "Waiting for check":
                        newStatus="Done";
                        break;
                    default:
                        break;
                }
                await dbContext.Documents
                    .Where(doc => doc.Name==documentName&&doc.Megadocumentid==Guid.Parse(documentMegaId))
                    .ExecuteUpdateAsync(doc => doc.SetProperty(e => e.Status, newStatus));
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

        }

        public async Task<bool> SendDocumentForRevisionAsync(byte[] documentBlob, string documentMegaId, string documentName, string documentStatus)
        {
            if (documentStatus!="Waiting for check")
            {
                return false;
            }
            try
            {
                var name = $"{documentMegaId} {documentName}";
                await megaRepository.DeleteDocumentByNameAsync(name);
                var documentData = await megaRepository.UploadDocumentAsync(documentBlob, documentName)??throw new Exception();
                var newStatus = "Sent to student";
                await dbContext.Documents
                    .Where(doc => doc.Name==documentName&&doc.Megadocumentid==Guid.Parse(documentMegaId))
                    .ExecuteUpdateAsync(doc => doc.SetProperty(e => e.Status, newStatus));
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
