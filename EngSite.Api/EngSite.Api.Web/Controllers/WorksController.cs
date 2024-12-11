using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork;
using EngSite.Api.DataAccess.Repository;
using EngSite.Api.Models.StudentTeacher;
using EngSite.Api.Models.Works;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EngSite.Api.Web.Controllers
{
    [ApiController]
    [Route("/works")]
    public class WorksController(ILogger<WorksController> logger,
        IWorksDocumentUnitOfWork worksDocumentUnitOfWork,
        IWorkDoumentRepository workDoumentRepository,
        IMegaRepository megaRepository) : ControllerBase
    {
        /// <summary>
        /// Добавить документ в хранилище
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addDocument")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddDocument([FromBody] AddDocumentRequest request)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                logger.LogInformation($"Login not found");
                return Unauthorized("Login not found");
            }
            var blob = Convert.FromBase64String(request.DocumentBase64);
            if (await worksDocumentUnitOfWork.AddDocumentAsync(blob, request.DocumentId, request.DocumentName, login, request.StudentLogin))
            {
                logger.LogInformation("Ok response");
                return Ok();
            }
            logger.LogInformation("400 error");
            return BadRequest();
        }

        /// <summary>
        /// Сохранить существующий документ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("saveDocument")]
        [Authorize(Roles = "User, Teacher")]
        public async Task<IActionResult> SaveDocument([FromBody] SaveDocumentRequest request)
        {
            var blob = Convert.FromBase64String(request.DocumentBase64);
            if (await worksDocumentUnitOfWork.SaveDocumentAsync(blob, request.DocumentMegaId, request.DocumentName, request.DocumentStatus))
            {
                logger.LogInformation("Ok response");
                return Ok();
            }
            logger.LogInformation("400 error");
            return BadRequest();
        }

        /// <summary>
        /// Поменять статус документа на следующий
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("sendDocument")]
        [Authorize(Roles = "User, Teacher")]
        public async Task<IActionResult> SendDocument([FromBody] SaveDocumentRequest request)
        {
            var blob = Convert.FromBase64String(request.DocumentBase64);
            if (await worksDocumentUnitOfWork.SendDocumentAsync(blob, request.DocumentMegaId, request.DocumentName, request.DocumentStatus))
            {
                logger.LogInformation("Ok response");
                return Ok();
            }
            logger.LogInformation("400 error");
            return BadRequest();
        }

        /// <summary>
        /// Установить статус документа как "отправить на доработку"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("sendDocumentForRevision")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> SendDocumentForRevision([FromBody] SaveDocumentRequest request)
        {
            var blob = Convert.FromBase64String(request.DocumentBase64);
            if (await worksDocumentUnitOfWork.SendDocumentAsync(blob, request.DocumentMegaId, request.DocumentName, request.DocumentStatus))
            {
                logger.LogInformation("Ok response");
                return Ok();
            }
            logger.LogInformation("400 error");
            return BadRequest();
        }

        /// <summary>
        /// Получить все документы
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getDocuments")]
        [Authorize(Roles = "Teacher, User")]
        public async Task<IActionResult> GetDocuments([FromQuery] GetDocumentsRequest request)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                logger.LogInformation($"Login not found");
                return Unauthorized("Login not found");
            }
            var docs = await workDoumentRepository.GetDocumentsDataAsync(request.TeacherLogin, request.StudentLogin);
            if (docs==null)
            {
                logger.LogError("Could not get documents");
                return BadRequest();
            }
            return Ok(docs);
        }

        /// <summary>
        /// Получить документ по имени
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getDocument")]
        [Authorize(Roles = "Teacher, User")]
        public async Task<IActionResult> GetDocument([FromQuery] GetDocumentRequest request)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                logger.LogInformation($"Login not found");
                return Unauthorized("Login not found");
            }
            var url = await workDoumentRepository.GetDocumentUrlAsync(request.DocumentId, request.DocumentName);
            var document = await megaRepository.GetDocumentAsync(url);
            var status = await workDoumentRepository.GetDocumentStatusAsync(request.DocumentId, request.DocumentName);
            if (document==null || status==null)
            {
                logger.LogError("Could not get documents");
                return BadRequest();
            }
            return File(document, "application/pdf");
        }

        /// <summary>
        /// Удалить документ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("deleteDocument")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> DeleteDocument([FromBody] DeleteDocumentRequest request)
        {
            if (await worksDocumentUnitOfWork.DeleteDocumentAsync(request.DocumentMegaId, request.DocumentName))
            {
                logger.LogError("Could not get documents");
                return Ok();
            }
            return BadRequest();
        }
    }
}
