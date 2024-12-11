using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Forum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EngSite.Api.Web.Controllers
{
    [Route("/forumControl")]
    [Authorize(Roles = "User, Teacher")]
    public class ForumController(IForumRepository forumRepository) : ControllerBase
    {
        /// <summary>
        /// Получить все сообщения в форуме
        /// </summary>
        /// <returns></returns>
        [HttpGet("getMessages")]
        public async Task<IActionResult> GetMessages()
        {
            var allForumMessages = await forumRepository.GetAllForumMessagesAsync();
            return Ok(allForumMessages);
        }


    }
}
