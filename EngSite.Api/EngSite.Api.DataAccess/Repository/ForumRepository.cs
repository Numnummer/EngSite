using AutoMapper;
using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Entities;
using EngSite.Api.Models.Forum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.DataAccess.Repository
{
    public class ForumRepository(EnglishSiteContext dbContext, IMapper mapper) : IForumRepository
    {
        public async Task<ForumMessage[]?> GetAllForumMessagesAsync()
        {
            var forum = await dbContext.Forums.ToArrayAsync();
            if (forum==null)
            {
                return null;
            }
            var forumMessages = new List<ForumMessage>();
            foreach (var item in forum)
            {
                forumMessages.Add(mapper.Map<ForumMessage>(item));
            }
            return forumMessages.ToArray();
        }

        public async Task<bool> PutForumMessageAsync(ForumMessage forumMessage)
        {
            try
            {
                var forum = mapper.Map<Forum>(forumMessage);
                forum.Id=Guid.NewGuid();
                await dbContext.Forums.AddAsync(forum);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
