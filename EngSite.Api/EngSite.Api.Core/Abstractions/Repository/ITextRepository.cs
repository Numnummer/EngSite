using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface ITextRepository : IDatabaseRepository
    {
        Task<bool> AddTextByUserLogin(string login, string textName, string path);
        Task<string[]> GetAllTextsForUserAsync(string userLogin);
        Task<string?> GetTextPathByNameAsync(string name);
    }
}
