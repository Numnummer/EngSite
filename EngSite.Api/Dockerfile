FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./EngSite.Api.Web/EngSite.Api.Web.csproj", "./EngSite.Api.Web/"]
COPY ["./EngSite.Api.DataAccess/EngSite.Api.DataAccess.csproj", "./EngSite.Api.DataAccess/"]
COPY ["./EngSite.Api.Core/EngSite.Api.BusinessLogic.csproj", "./EngSite.Api.Core/"]
COPY ["./EngSite.Api.Contracts/EngSite.Api.Models.csproj", "./EngSite.Api.Contracts/"]
RUN dotnet restore "EngSite.Api.Web/EngSite.Api.Web.csproj"
COPY . .
WORKDIR "/src/EngSite.Api.Web"
RUN dotnet build "EngSite.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EngSite.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final  
WORKDIR /app    
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EngSite.Api.Web.dll"]
