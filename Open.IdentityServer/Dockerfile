FROM node:12-alpine AS node

ENV ASPNETCORE_URLS="http://+:80"

WORKDIR "/src"
COPY ["Open.IdentityServer/ClientApp/package.json", "Open.IdentityServer/ClientApp/"]
COPY ["Open.IdentityServer/ClientApp/package-lock.json", "Open.IdentityServer/ClientApp/"]

WORKDIR "/src/Open.IdentityServer/ClientApp"

RUN npm install

WORKDIR "/src"
COPY ["Open.IdentityServer/ClientApp/", "Open.IdentityServer/ClientApp/"]

WORKDIR "/src/Open.IdentityServer/ClientApp"
RUN npm run build

FROM  mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR "/app"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR "/src"
COPY ["Open.IdentityServer/Open.IdentityServer.csproj", "Open.IdentityServer/"]
COPY ["Open.IdentityServer.Domain/Open.IdentityServer.Domain.csproj", "Open.IdentityServer.Domain/"]
COPY ["Open.IdentityServer.Infrastructure/Open.IdentityServer.Infrastructure.csproj", "Open.IdentityServer.Infrastructure/"]

COPY ["Open.IdentityServer.Migrations.MSSQL/Open.IdentityServer.Migrations.MSSQL.csproj", "Open.IdentityServer.Migrations.MSSQL/"]
COPY ["Open.IdentityServer.Migrations.PostgreSQL/Open.IdentityServer.Migrations.PostgreSQL.csproj", "Open.IdentityServer.Migrations.PostgreSQL/"]
COPY ["Open.IdentityServer.Migrations.MySQL/Open.IdentityServer.Migrations.MySQL.csproj", "Open.IdentityServer.Migrations.MySQL/"]
COPY ["Open.IdentityServer.Migrations.SQLite/Open.IdentityServer.Migrations.SQLite.csproj", "Open.IdentityServer.Migrations.SQLite/"]

RUN dotnet restore "Open.IdentityServer/Open.IdentityServer.csproj"

COPY . .
WORKDIR "/src/Open.IdentityServer"
RUN dotnet build "Open.IdentityServer.csproj" -c Release -o /app/build

COPY --from=node /src/Open.IdentityServer/ClientApp/dist/ /src/Open.IdentityServer/wwwroot/

FROM build AS publish
RUN dotnet publish "Open.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR "/app"
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet"]
CMD ["Open.IdentityServer.dll"]

EXPOSE 80
