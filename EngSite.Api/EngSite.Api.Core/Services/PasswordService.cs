using EngSite.Api.BusinessLogic.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Services
{
    public class PasswordService(IConfiguration configuration) : IPasswordService
    {
        public string GetPasswordHash(string password)
        {
            var salt = configuration["PasswordSettings:Salt"];
            var encodedInput = Encoding.UTF8.GetBytes(password);
            var encodedSalt = Encoding.UTF8.GetBytes(salt);
            var iterationCount = 10000;
            using (var pbkdf2 = new Rfc2898DeriveBytes(encodedInput, encodedSalt, iterationCount, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
