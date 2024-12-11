using EngSite.Api.Models.Mega;
using EngSite.Api.Models.Texts;
using EngSite.Api.Models.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IMegaRepository
    {
        string? GetTextsFolderNameByLevel(string level);
        Task<DownloadedTextFile> DownloadRandomFileFromFolderAsync(string folder);
        Task<string?> GetFilePathByTextDataAsync(TextData textData);
        Task<DownloadedTextFile?> DownloadFileAsync(string filePath);
        Task<UploadedDocumentData?> UploadDocumentAsync(byte[] documentBlob, string documentName);
        Task<bool> DeleteDocumentAsync(string filePath);
        Task<bool> DeleteDocumentByNameAsync(string documentName);
        Task<byte[]?> GetDocumentAsync(string link);
        Task<bool> IsDocumentExistsAsync(string documentName);
    }
}
