using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork;
using EngSite.Api.Models.Entities;
using EngSite.Api.Models.User.Registrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.DataAccess.UnitOfWork
{
    public class RegistrationUnitOfWork(EnglishSiteContext dbContext,
        IPasswordService passwordService, ILogger<RegistrationUnitOfWork> logger) : IRegistrationUnitOfWork
    {
        public async Task<bool> RegistrateUserAsync(User userData)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                userData.Password = passwordService.GetPasswordHash(userData.Password);
                await dbContext.Users.AddAsync(userData);

                var userStat = await dbContext.UserStats.FirstOrDefaultAsync(u => u.UserLogin==userData.Login);
                if (userStat!=null)
                {
                    return false;
                }
                var obj = new UserStat()
                {
                    UserLogin = userData.Login,
                    AudioListened=0,
                    GrammarProgression=0,
                    TextsRead=0,
                    VideoWatched=0,
                    WordsLearned=0,
                    Id=Guid.NewGuid(),
                };
                await dbContext.UserStats.AddAsync(obj);

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
