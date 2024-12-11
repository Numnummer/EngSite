using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.Models.Mega;
using EngSite.Api.Models.Texts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Services
{
    public class TextService(IMegaRepository megaRepository,
        ITextRepository textRepository) : ITextService
    {
        public async Task<DownloadedTextFile> GetRandomTextByLevelAsync(string level)
        {
            string? folder = megaRepository.GetTextsFolderNameByLevel(level);
            if (folder==null)
            {
                return null;
            }
            return await megaRepository.DownloadRandomFileFromFolderAsync(folder);
        }

        public async Task<DownloadedTextFile?> GetTextByNameAsync(string name)
        {
            var path = await textRepository.GetTextPathByNameAsync(name);
            if (path==null)
            {
                return null;
            }
            return await megaRepository.DownloadFileAsync(path);
        }

        public async Task<bool> SaveTextToUserAsync(string login, TextData textData)
        {
            var textPath = await megaRepository.GetFilePathByTextDataAsync(textData);
            if (textPath==null)
            {
                return false;
            }
            if (await textRepository.AddTextByUserLogin(login, textData.TextName, textPath))
            {
                return true;
            }
            return false;
        }
    }
}
