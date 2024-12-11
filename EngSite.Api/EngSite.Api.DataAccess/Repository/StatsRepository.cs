using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.DataAccess.Repository
{
    public class StatsRepository(EnglishSiteContext dbContext,
        ILogger<StatsRepository> logger) : IStatsRepository
    {
        public async Task<bool> AddWordsLearnedAsync(string login, int wordsLearned)
        {
            try
            {
                var updated = await dbContext.UserStats.
                    Where(u => u.UserLogin==login).
                    ExecuteUpdateAsync(e =>
                    e.SetProperty(u => u.WordsLearned, u => u.WordsLearned+wordsLearned));
                if (updated==1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<UserStat?> GetUserStatAsync(string login)
        {
            return await dbContext.UserStats.FirstOrDefaultAsync(u => u.UserLogin==login);
        }

        [Obsolete("Use IRegistrationUnitOfWork")]
        public async Task<bool> InitUserStatsAsync(string login)
        {
            try
            {
                var userStat = await dbContext.UserStats.FirstOrDefaultAsync(u => u.UserLogin==login);
                if (userStat!=null)
                {
                    return false;
                }
                var obj = new UserStat()
                {
                    UserLogin = login,
                    AudioListened=0,
                    GrammarProgression=0,
                    TextsRead=0,
                    VideoWatched=0,
                    WordsLearned=0,
                    Id=Guid.NewGuid(),
                };
                await dbContext.UserStats.AddAsync(obj);
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
