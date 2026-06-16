using BeerBrewery.Domain.Interfaces;
using BeerBrewery.Infrastructure.Data;
using BeerBrewery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection RegisterInfrastructure(
            this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBeerRepository, BeerRepository>();

            return services;
        }
    }
}
