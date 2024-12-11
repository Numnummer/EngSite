using EngSite.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IStatsRepository : IDatabaseRepository
    {
        Task<UserStat?> GetUserStatAsync(string login);
        Task<bool> AddWordsLearnedAsync(string login, int wordsLearned);
        [Obsolete("Use IRegistrationUnitOfWork")]
        Task<bool> InitUserStatsAsync(string login);
    }
}
