using EngSite.Api.Models.Mega;
using EngSite.Api.Models.Texts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Services
{
    public interface ITextService
    {
        Task<DownloadedTextFile> GetRandomTextByLevelAsync(string level);
        Task<bool> SaveTextToUserAsync(string login, TextData textData);
        Task<DownloadedTextFile> GetTextByNameAsync(string name);
    }
}
