using CG.Web.MegaApiClient;
using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.Models.Mega;
using EngSite.Api.Models.Texts;
using EngSite.Api.Models.Works;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EngSite.Api.DataAccess.Repository
{
    public class MegaRepository(IConfiguration configuration,
        ILogger<MegaRepository> logger) : IMegaRepository
    {
        private readonly string? _email = configuration["MegaSettings:email"];
        private readonly string? _password = configuration["MegaSettings:password"];
        private readonly MegaApiClient _client = new();
        public string? GetTextsFolderNameByLevel(string level)
        {
            switch (level)
            {
                case "A1":
                    return configuration["MegaSettings:TextsFolder:A1"];
                case "A2":
                    return configuration["MegaSettings:TextsFolder:A2"];
                case "B1":
                    return configuration["MegaSettings:TextsFolder:B1"];
                case "B2":
                    return configuration["MegaSettings:TextsFolder:B2"];
                case "C1":
                    return configuration["MegaSettings:TextsFolder:C1"];
                default:
                    return null;
            }
        }
        public async Task<DownloadedTextFile> DownloadRandomFileFromFolderAsync(string folder)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var files = await _client.GetNodesFromLinkAsync(new Uri(folder));

                var random = new Random();
                int randomIndex = random.Next(0, files.Count());
                var randomFile = files.ElementAt(randomIndex);

                using var stream = await _client.DownloadAsync(randomFile);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                return new DownloadedTextFile()
                {
                    Data=Encoding.UTF8.GetString(memoryStream.ToArray()),
                    FileName= randomFile.Name
                };
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<string?> GetFilePathByTextDataAsync(TextData textData)
        {
            var folderPath = GetTextsFolderNameByLevel(textData.TextLevel);
            if (folderPath == null)
            {
                return null;
            }
            try
            {
                await _client.LoginAsync(_email, _password);
                var files = await _client.GetNodesFromLinkAsync(new Uri(folderPath));
                var node = files.FirstOrDefault(node => node.Name==(textData.TextName));
                if (node == null)
                {
                    return null;
                }
                var filePath = await _client.GetDownloadLinkAsync(node);

                return filePath.ToString();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<DownloadedTextFile?> DownloadFileAsync(string filePath)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var node = await _client.GetNodeFromLinkAsync(new Uri(filePath));
                if (node == null)
                {
                    return null;
                }
                using var stream = await _client.DownloadAsync(node);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                return new DownloadedTextFile()
                {
                    Data=Encoding.UTF8.GetString(memoryStream.ToArray()),
                    FileName= node.Name
                };
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<UploadedDocumentData?> UploadDocumentAsync(byte[] documentBlob, string documentName)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var folder = configuration["MegaSettings:WorksFolder"];
                var folderNode = (await _client.GetNodesAsync()).First(node => node.Name=="Works");

                using var stream = new MemoryStream(documentBlob);
                var documentId = Guid.NewGuid();
                await _client.UploadAsync(stream, $"{documentId} ${documentName}", folderNode);
                var nodes = await _client.GetNodesAsync(folderNode);
                var node = nodes.FirstOrDefault(node => node.Name==$"{documentId} ${documentName}");
                var link = await _client.GetDownloadLinkAsync(node);
                return new UploadedDocumentData()
                {
                    Link=link.ToString(),
                    Id=documentId,
                };
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<bool> DeleteDocumentAsync(string filePath)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var node = await _client.GetNodeFromLinkAsync(new Uri(filePath));
                await _client.DeleteAsync(node);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<byte[]?> GetDocumentAsync(string link)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var node = await _client.GetNodeFromLinkAsync(new Uri(link));
                using var stream = await _client.DownloadAsync(node);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<bool> IsDocumentExistsAsync(string documentName)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var folder = configuration["MegaSettings:WorksFolder"];
                var nodes = await _client.GetNodesAsync();
                return nodes.Any(node => node.Name == documentName);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }

        public async Task<bool> DeleteDocumentByNameAsync(string documentName)
        {
            try
            {
                await _client.LoginAsync(_email, _password);
                var folder = configuration["MegaSettings:WorksFolder"];
                var nodes = await _client.GetNodesAsync();
                var node = nodes.FirstOrDefault(node => node.Name == documentName);
                await _client.DeleteAsync(node);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
            finally
            {
                await _client.LogoutAsync();
            }
        }
    }
}
