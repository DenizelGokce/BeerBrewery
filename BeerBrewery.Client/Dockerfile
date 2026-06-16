FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BeerBrewery.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
RUN mkdir -p /app/data && chown -R app:app /app/data
COPY --from=publish /app/publish .
USER app
ENTRYPOINT ["dotnet", "BeerBrewery.Client.dll"]