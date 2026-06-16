using BeerBrewery.Application.Interfaces;
using BeerBrewery.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BeerBrewery.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<IBeerService, BeerService>();
        return services;
    }
}