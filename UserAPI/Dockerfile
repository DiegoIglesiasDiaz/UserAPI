# Fase base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080  
EXPOSE 8081  

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["../Application/Application.csproj", "Application/"]
COPY ["../Domain/Domain.csproj", "Domain/"]
COPY ["../Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["../UserAPI/UserAPI.csproj", "UserAPI/"]

RUN dotnet restore "UserAPI/UserAPI.csproj"
COPY ../ .
WORKDIR "/src/UserAPI"
RUN dotnet build "UserAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final para ejecución
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the environment variable for HTTPS
ENV ASPNETCORE_HTTPS_PORT 8081

# Configure entry point
ENTRYPOINT ["dotnet", "UserAPI.dll"]
