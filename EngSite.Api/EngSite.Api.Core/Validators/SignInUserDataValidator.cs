using EngSite.Api.Models.User.SignIn;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngSite.Api.BusinessLogic.Validators
{
    public class SignInUserDataValidator : AbstractValidator<SignInUserData>
    {
        public SignInUserDataValidator()
        {
            RuleFor(prop => prop.Login).NotEmpty();
            RuleFor(data => data.Password).MinimumLength(6);
            RuleFor(data => data.Password)
            .Must(data => ContainsUpperCase(data) && ContainsLowerCase(data));
        }
        private bool ContainsUpperCase(string value)
        {
            return value.Any(char.IsUpper);
        }

        private bool ContainsLowerCase(string value)
        {
            return value.Any(char.IsLower);
        }
    }
}
