# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BeerBrewery.Client/BeerBrewery.Client.csproj", "BeerBrewery.Client/"]
COPY ["BeerBrewery.Application/BeerBrewery.Application.csproj", "BeerBrewery.Application/"]
COPY ["BeerBrewery.Domain/BeerBrewery.Domain.csproj", "BeerBrewery.Domain/"]
COPY ["BeerBrewery.Infrastructure/BeerBrewery.Infrastructure.csproj", "BeerBrewery.Infrastructure/"]
RUN dotnet restore "./BeerBrewery.Client/BeerBrewery.Client.csproj"
COPY . .
WORKDIR "/src/BeerBrewery.Client"
RUN dotnet build "./BeerBrewery.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BeerBrewery.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeerBrewery.Client.dll"]