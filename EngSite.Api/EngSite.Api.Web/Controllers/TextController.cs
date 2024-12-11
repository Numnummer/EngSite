using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.Models.Texts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace EngSite.Api.Web.Controllers
{
    [ApiController]
    [Route("/text")]
    [Authorize(Roles = "User, Teacher")]
    public class TextController(ITextService textService, ITextRepository textRepository) : ControllerBase
    {
        /// <summary>
        /// Получить рандомный текст из хранилища 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpGet("getRandomText/{level}")]
        public async Task<IActionResult> GetRandomTextAsync(string level)
        {
            var file = await textService.GetRandomTextByLevelAsync(level);
            if (file == null)
            {
                return StatusCode(500);
            }
            return Ok(file);
        }

        /// <summary>
        /// Привязать текст к пользователю
        /// </summary>
        /// <param name="textData"></param>
        /// <returns></returns>
        [HttpPost("addText")]
        public async Task<IActionResult> AddTextToUserAsync(TextData textData)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            if (textData==null)
            {
                return BadRequest("CannotDeserializeTextData");
            }
            if (await textService.SaveTextToUserAsync(login, textData))
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Получить все тексты пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllTexts")]
        public async Task<IActionResult> GetAllTextsAsync()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            var texts = await textRepository.GetAllTextsForUserAsync(login);
            return Ok(texts);
        }

        /// <summary>
        /// Получить текст по названию из хранилища
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("getText/{name}")]
        public async Task<IActionResult> GetText(string name)
        {
            var file = await textService.GetTextByNameAsync(name);
            if (file==null)
            {
                return StatusCode(500);
            }
            return Ok(file);
        }
    }
}
