#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# The correct way to make sure we tighten security in the container by 
# not allowing it to execute in the default context of root user

# 1. Add a securitygroup named app, already made by Ms with the silly value of 1654
# RUN groupadd --gid 1654 app
# 2. Add a user named app and add it to group app, already made by Ms with the silly value of 1654
# RUN useradd -u 1654 -g app app
# 3. Declare our running user
USER app
# 4. Look further in deployment.yaml...
WORKDIR /data
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG Configuration
WORKDIR /src
COPY ["BuildVersionsApi/BuildVersionsApi.csproj", "BuildVersionsApi/"]
COPY ["BuildVersionsApi.Domain/BuildVersionsApi.Domain.csproj", "BuildVersionsApi.Domain/"]
COPY ["BuildVersionsApi.Features/BuildVersionsApi.Features.csproj", "BuildVersionsApi.Features/"]
COPY ["BuildVersionsApi.Persistance/BuildVersionsApi.Persistance.csproj", "BuildVersionsApi.Persistance/"]
COPY ["BuildVersionsApi.Diagnostics/BuildVersionsApi.Diagnostics.csproj", "BuildVersionsApi.Diagnostics/"]
COPY ["Auth.Module/Auth.Module.csproj", "Auth.Module/"]
RUN dotnet restore "./BuildVersionsApi/BuildVersionsApi.csproj"
COPY . .
WORKDIR "/src/BuildVersionsApi"
RUN dotnet build "./BuildVersionsApi.csproj" -c "$Configuration" -o /app/build

FROM build AS publish
ARG Configuration
ARG Version
ARG Description
RUN dotnet publish "./BuildVersionsApi.csproj" -c "$Configuration" -o /app/publish /p:UseAppHost=false -p:Version="$Version" -p:Description="$Description" 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildVersionsApi.dll"]