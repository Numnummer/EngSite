using EngSite.Api.BusinessLogic.Abstractions.Repository;
using EngSite.Api.BusinessLogic.Abstractions.Services;
using EngSite.Api.BusinessLogic.Abstractions.UnitsOfWork;
using EngSite.Api.BusinessLogic.Services;
using EngSite.Api.BusinessLogic.Validators;
using EngSite.Api.DataAccess.Repository;
using EngSite.Api.DataAccess.UnitOfWork;
using EngSite.Api.Models.User.Registrate;
using EngSite.Api.Models.User.SignIn;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace EngSite.Api.Web.Extentions
{
    public static class WebApplicationExtentions
    {
        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            var issuer = builder.Configuration["JwtSettings:Issuer"];
            var audience = builder.Configuration["JwtSettings:Audience"];
            var secretKey = builder.Configuration["JwtSettings:SecretKey"]!;

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = signingKey,
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWordsRepository, WordsRepository>();
            builder.Services.AddScoped<IMegaRepository, MegaRepository>();
            builder.Services.AddScoped<ITextRepository, TextRepository>();
            builder.Services.AddScoped<IStatsRepository, StatsRepository>();
            builder.Services.AddScoped<IForumRepository, ForumRepository>();
            builder.Services.AddScoped<IStudentTeacherRepository, StudentTeacherRepository>();
            builder.Services.AddScoped<IWorkDoumentRepository, WorkDocumentRepository>();
        }
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordService, PasswordService>();
            builder.Services.AddScoped<ITextService, TextService>();
        }
        public static void AddValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<RegistrationUserData>, RegistrationUserDataValidator>();
            builder.Services.AddScoped<IValidator<SignInUserData>, SignInUserDataValidator>();
        }

        public static void AddUnitsOfWork(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRegistrationUnitOfWork, RegistrationUnitOfWork>();
            builder.Services.AddScoped<IWorksDocumentUnitOfWork, WorksDocumentUnitOfWork>();
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}
