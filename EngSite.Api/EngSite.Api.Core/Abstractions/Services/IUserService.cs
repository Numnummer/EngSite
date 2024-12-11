using EngSite.Api.Models.Entities;
using EngSite.Api.Models.User.Registrate;
using EngSite.Api.Models.User.SignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> GetValidUserAsync(RegistrationUserData userData);
        Task<string?> SignInUserAsync(SignInUserData userData);
        string MakeJwtToken(SignInUserData userData, string role);
    }
}
