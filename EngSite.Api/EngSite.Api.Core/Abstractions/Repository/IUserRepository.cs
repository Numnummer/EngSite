using EngSite.Api.Models.Entities;
using EngSite.Api.Models.User.GetUserData;
using EngSite.Api.Models.User.Registrate;
using EngSite.Api.Models.User.SignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Repository
{
    public interface IUserRepository : IDatabaseRepository
    {
        Task<bool> AddNewUserAsync(User userData);
        Task<bool> IsUserExists(SignInUserData signInUserData);
        Task<string> GetUserRoleAsync(string login);
        Task<User?> GetUserDataAsync(string login);
        Task<bool> PostPhotoToUser(string login, byte[] photoBlob);
    }
}
