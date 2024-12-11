using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IWordsRepository : IDatabaseRepository
    {
        Task<string?> GetRandomWordAsync();
        Task<string[]?> GetRandomDictionaryWordsAsync(int count);
        Task<string[][]?> GetRandomUserDictionaryWordsAsync(string userLogin, int count);
        Task<bool> AddWordsAndTranslationsIfNotExistsAsync(string[][] wordsAndTranslations, string userLogin);
        Task<string[][]> GetWordsAndTranslationsOfUserAsync(string userLogin);
        Task<bool> RemoveSentenceAsync(string userLogin, string[] sentence);
    }
}
