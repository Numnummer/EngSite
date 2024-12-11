using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EngSite.Api.Web.Controllers
{
    [ApiController]
    [Route("/translate")]
    [Authorize(Roles = "User,Teacher")]
    public class TranslationController : ControllerBase
    {
        /// <summary>
        /// Перевести с английского на русский
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [HttpGet("enRu")]
        public async Task<IActionResult> TranslateEnglishSentence([FromQuery] string sentence)
        {
            var client = new HttpClient();
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=ru&dt=t&q={sentence}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }

        /// <summary>
        /// Перевести с русского на английский
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        [HttpGet("ruEn")]
        public async Task<IActionResult> TranslateRissianSentence([FromQuery] string sentence)
        {
            var client = new HttpClient();
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ru&tl=en&dt=t&q={sentence}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return Content(responseContent, "application/json");
            }

            return StatusCode((int)response.StatusCode);
        }
    }
}
