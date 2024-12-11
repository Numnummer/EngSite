using EngSite.Api.Models.User.Registrate;
using FluentValidation;

namespace EngSite.Api.BusinessLogic.Validators
{
    public class RegistrationUserDataValidator : AbstractValidator<RegistrationUserData>
    {
        public RegistrationUserDataValidator()
        {
            RuleFor(data => data.FullName).NotEmpty();
            RuleFor(data => data.Email).NotEmpty().EmailAddress();
            RuleFor(data => data.Login).NotEmpty();
            RuleFor(data => data.Password).MinimumLength(6);
            RuleFor(data => data.Password)
            .Must(data => ContainsUpperCase(data) && ContainsLowerCase(data));
            RuleFor(data => data.Confirm).Equal(data => data.Password);
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