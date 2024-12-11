using AutoMapper;
using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.Models.Entities;
using EngSite.Api.Models.User.Registrate;
using EngSite.Api.Models.User.SignIn;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EngSite.Api.BusinessLogic.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper,
        IValidator<RegistrationUserData> registrationValidator,
        IValidator<SignInUserData> signInValidator,
        IConfiguration configuration) : IUserService
    {
        public string MakeJwtToken(SignInUserData userData, string role)
        {
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, userData.Login),
                new(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["JwtSettings:AccessTokenLifetimeInMinutes"])),
                signingCredentials: sign
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> GetValidUserAsync(RegistrationUserData userData)
        {
            if (!(await registrationValidator.ValidateAsync(userData)).IsValid)
            {
                return null;
            }
            var user = mapper.Map<User>(userData);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<string?> SignInUserAsync(SignInUserData userData)
        {
            if ((await signInValidator.ValidateAsync(userData)).IsValid
                    && await userRepository.IsUserExists(userData))
            {
                var role = await userRepository.GetUserRoleAsync(userData.Login);
                return MakeJwtToken(userData, role);
            }
            return null;
        }
    }
}