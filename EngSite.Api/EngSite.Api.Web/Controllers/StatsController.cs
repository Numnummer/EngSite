using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Stats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EngSite.Api.Web.Controllers
{
    /// <summary>
    /// Контроллер для статистик
    /// </summary>
    [ApiController]
    [Route("/stats")]
    [Authorize(Roles = "User, Teacher")]
    public class StatsController(IStatsRepository statsRepository,
        ILogger<StatsController> logger) : ControllerBase
    {
        /// <summary>
        /// Увеличить количество изученных слов
        /// </summary>
        /// <returns></returns>
        [HttpPost("addLearnedWords")]
        public async Task<IActionResult> AddLearnedWords(AddLearnedWordsRequest body)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            if (await statsRepository.AddWordsLearnedAsync(login, body.LearnedWords))
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Инициализирует статистику, устарел, статистика инициализируется
        /// во время регистрации
        /// </summary>
        /// <returns></returns>
        [Obsolete("Stats initialises in RegistrateUser (UserController)")]
        [HttpGet("init")]
        public async Task<IActionResult> Init()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            if (await statsRepository.InitUserStatsAsync(login))
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Получить статистику пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("getStats")]
        public async Task<IActionResult> GetUserStat()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            var stat = await statsRepository.GetUserStatAsync(login);
            return Ok(stat);
        }
    }
}
