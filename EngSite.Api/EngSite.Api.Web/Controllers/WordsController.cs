using EngSite.Api.BusinessLogic.Abstractions.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EngSite.Api.Web.Controllers
{
    [ApiController]
    [Route("/words")]
    [Authorize(Roles = "User, Teacher")]
    public class WordsController(IWordsRepository wordsRepository) : ControllerBase
    {
        /// <summary>
        /// Получить рандомное английское слово
        /// </summary>
        /// <returns></returns>
        [HttpGet("getRandomWord")]
        public async Task<IActionResult> GetRandomWordAsync()
        {
            return Ok(await wordsRepository.GetRandomWordAsync());
        }

        /// <summary>
        /// Получить несколько английских слов
        /// </summary>
        /// <param name="count">Нужное количество слов</param>
        /// <returns></returns>
        [HttpGet("getRandomDictionaryWords/{count}")]
        public async Task<IActionResult> GetRandomDictionaryWordsAsync(int count)
        {
            return Ok(await wordsRepository.GetRandomDictionaryWordsAsync(count));
        }

        /// <summary>
        /// Получить несколько английских слов из словаря пользователя
        /// </summary>
        /// <param name="count">Нужное количество слов</param>
        /// <returns></returns>
        [HttpGet("getRandomUserDictionaryWords/{count}")]
        public async Task<IActionResult> GetRandomUserDictionaryWordsAsync(int count)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            return Ok(await wordsRepository.GetRandomUserDictionaryWordsAsync(login, count));
        }

        /// <summary>
        /// Добавить слова в словарь пользователя
        /// </summary>
        /// <param name="body">Массив слов в виде [Слово, перевод]</param>
        /// <returns></returns>
        [HttpPost("setWords")]
        public async Task<IActionResult> SetWordsAsync(string[][] body)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            if (await wordsRepository.AddWordsAndTranslationsIfNotExistsAsync(body, login))
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Получить слова из словаря
        /// </summary>
        /// <returns>Массив слов в виде [Слово, перевод]</returns>
        [HttpGet("getWords")]
        public async Task<IActionResult> GetWordsAsync()
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            return Ok(await wordsRepository.GetWordsAndTranslationsOfUserAsync(login));
        }

        /// <summary>
        /// Удалить [Слово, перевод] из словаря
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [HttpPost("removeSentence")]
        public async Task<IActionResult> RemoveSentenceAsync(string[] sentence)
        {
            var login = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (login==null)
            {
                return Unauthorized("Login not found");
            }
            if (await wordsRepository.RemoveSentenceAsync(login, sentence))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
