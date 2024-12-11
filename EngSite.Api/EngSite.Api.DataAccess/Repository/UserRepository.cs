using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.Models.Entities;
using EngSite.Api.Models.User.GetUserData;
using EngSite.Api.Models.User.SignIn;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EngSite.Api.DataAccess.Repository
{
    public class UserRepository(EnglishSiteContext dbContext,
        IPasswordService passwordService, ILogger<UserRepository> logger) : IUserRepository
    {
        [Obsolete("Use IRegistrationUnitOfWork")]
        public async Task<bool> AddNewUserAsync(User userData)
        {
            try
            {
                userData.Password = passwordService.GetPasswordHash(userData.Password);
                await dbContext.Users.AddAsync(userData);
                return true;
            }
            catch (OperationCanceledException e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<User?> GetUserDataAsync(string login)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<string?> GetUserRoleAsync(string login)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == login);
                if (user == null)
                {
                    return null;
                }
                return user.Role;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> IsUserExists(SignInUserData signInUserData)
        {
            try
            {
                var hashedPassword = passwordService.GetPasswordHash(signInUserData.Password);
                return await dbContext.Users.
                    AnyAsync(user =>
                        user.Login==signInUserData.Login
                        && user.Password==hashedPassword);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> PostPhotoToUser(string login, byte[] photoBlob)
        {
            try
            {
                var updated = await dbContext.Users.Where(u => u.Login == login).ExecuteUpdateAsync(e => e.SetProperty(u => u.Photo, p => photoBlob));
                if (updated==1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}