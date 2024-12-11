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
    public class TextRepository(EnglishSiteContext dbContext,
        ILogger<TextRepository> logger) : ITextRepository
    {
        public async Task<bool> AddTextByUserLogin(string login, string textName, string path)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                var textId = Guid.NewGuid();
                await dbContext.Texts.AddAsync(new Text()
                {
                    Id=textId,
                    Name=textName,
                    Path=path
                });
                await dbContext.UserTexts.AddAsync(new UserText()
                {
                    TextId=textId,
                    UserLogin=login,
                });
                await SaveChangesAsync();
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

        public async Task<string[]> GetAllTextsForUserAsync(string userLogin)
        {
            return await dbContext.UserTexts.Where(e => e.UserLogin==userLogin).Select(e => e.Text.Name).ToArrayAsync();
        }

        public async Task<string?> GetTextPathByNameAsync(string name)
        {
            var text = await dbContext.Texts.FirstOrDefaultAsync(text => text.Name==name);
            return text?.Path;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
