using EngSite.Api.DataAccess;
using EngSite.Api.Models.Forum;
using EngSite.Api.Web.Extentions;
using EngSite.Api.Web.Hubs;
using EngSite.Api.Web.MapperProfiles;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EnglishSiteContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDbConnect")));
builder.Services.AddAutoMapper(mapper => mapper.AddProfile<AppMapProfile>());
builder.AddJwtAuthentication();
builder.Services.AddAuthorization();
builder.AddRepositories();
builder.AddValidators();
builder.AddServices();
builder.AddUnitsOfWork();
builder.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowFrontendOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ForumHub>("/forum");


app.Run();
