using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Forum;
using Microsoft.AspNetCore.SignalR;

namespace EngSite.Api.Web.Hubs
{
    public class ForumHub(IConfiguration configuration,
        IForumRepository forumRepository) : Hub
    {
        public async Task SendMessageAsync(string author, string message, DateTime date)
        {
            var method = configuration["SignalrMethods:Recieve"];
            await Clients.All.SendAsync(method, author, message, date);
            var forumMessage = new ForumMessage()
            {
                DateTime = DateTime.UtcNow,
                Author = author,
                Message = message
            };
            await forumRepository.PutForumMessageAsync(forumMessage);
        }
    }
}
