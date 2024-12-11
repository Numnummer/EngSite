using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.DataAccess.Repository
{
    public class WordsRepository(EnglishSiteContext dbContext,
        ILogger<WordsRepository> logger) : IWordsRepository
    {
        public const int FirstWordId = 4319;
        public const int LastWordId = 8636;

        public async Task<bool> AddWordsAndTranslationsIfNotExistsAsync(string[][] wordsAndTranslations, string userLogin)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                var dictionaries = await SelectOnlyNewWordsAndTranslationsAsync(wordsAndTranslations);
                var userDictionaryData = dictionaries.Select(item => new UserDictionary()
                {
                    UserLogin = userLogin,
                    SentenceId=item.Id
                }).ToArray();
                await dbContext.Dictionaries.AddRangeAsync(dictionaries);
                await dbContext.UserDictionaries.AddRangeAsync(userDictionaryData);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex.Message);
                return false;
            }
        }

        private async Task<List<Dictionary>> SelectOnlyNewWordsAndTranslationsAsync(string[][] wordsAndTranslations)
        {
            var dictionariesToAdd = new List<Dictionary>();
            foreach (var item in wordsAndTranslations)
            {
                var dictionary = await dbContext.Dictionaries.FirstOrDefaultAsync(d => d.Sentence == item[0] && d.Translation == item[1]);
                if (dictionary == null)
                {
                    dictionariesToAdd.Add(new Dictionary()
                    {
                        Sentence = item[0],
                        Translation = item[1],
                        Id=Guid.NewGuid()
                    });
                }
            }
            return dictionariesToAdd;
        }

        public async Task<string?> GetRandomWordAsync()
        {
            try
            {
                var random = new Random();
                var index = random.Next(FirstWordId, LastWordId+1);
                var word = (await dbContext.WordStorages.FirstOrDefaultAsync(word => word.Id == index))?.Word;
                if (word == null)
                {
                    throw new Exception();
                }
                return word;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<string[][]> GetWordsAndTranslationsOfUserAsync(string userLogin)
        {
            return await dbContext.UserDictionaries
                .Where(entity => entity.UserLogin == userLogin)
                .Select(entity =>
                    new string[2]
                    {
                        entity.Sentence.Sentence, entity.Sentence.Translation
                    })
                .ToArrayAsync();
        }

        public async Task<bool> RemoveSentenceAsync(string userLogin, string[] sentence)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            try
            {
                var dictionaryToRemove = await dbContext.Dictionaries.FirstOrDefaultAsync(dict => dict.Sentence==sentence[0] && dict.Translation==sentence[1]);
                if (dictionaryToRemove==null)
                {
                    throw new Exception("dictionaryToRemove is null");
                }
                var userDictToRemove = await dbContext.UserDictionaries.FirstOrDefaultAsync(entity => entity.UserLogin==userLogin && entity.Sentence.Id==dictionaryToRemove.Id);
                if (userDictToRemove==null)
                {
                    throw new Exception("userDictToRemove is null");
                }
                dbContext.UserDictionaries.Remove(userDictToRemove);
                dbContext.Dictionaries.Remove(dictionaryToRemove);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<string[][]?> GetRandomUserDictionaryWordsAsync(string userLogin, int count)
        {
            var words = await dbContext.UserDictionaries.Where(userD => userD.UserLogin==userLogin).Include(e => e.Sentence).ToListAsync();
            if (words.Count<=count)
            {
                count=words.Count;
            }
            var random = new Random();
            var result = new List<string[]>();
            for (int i = 0; i < count; i++)
            {
                var index = random.Next(words.Count);
                var word = words[index].Sentence;
                result.Add(
                [
                    word.Sentence,word.Translation
                ]);
            }

            return result.ToArray();
        }

        public async Task<string[]?> GetRandomDictionaryWordsAsync(int count)
        {
            var words = await dbContext.WordStorages.ToListAsync();
            if (words.Count<=count)
            {
                count=words.Count;
            }
            var random = new Random();
            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var index = random.Next(words.Count);
                var word = words[index];
                result.Add(word.Word);
            }

            return result.ToArray();
        }
    }
}
