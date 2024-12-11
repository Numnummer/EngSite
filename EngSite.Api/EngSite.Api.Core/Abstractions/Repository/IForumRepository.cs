using EngSite.Api.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IForumRepository : IDatabaseRepository
    {
        Task<ForumMessage[]?> GetAllForumMessagesAsync();
        Task<bool> PutForumMessageAsync(ForumMessage forumMessage);
    }
}
