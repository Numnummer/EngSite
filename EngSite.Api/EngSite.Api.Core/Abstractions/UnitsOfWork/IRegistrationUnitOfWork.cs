using EngSite.Api.Models.Entities;
using EngSite.Api.Models.User.Registrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork
{
    public interface IRegistrationUnitOfWork
    {
        Task<bool> RegistrateUserAsync(User userData);
    }
}
