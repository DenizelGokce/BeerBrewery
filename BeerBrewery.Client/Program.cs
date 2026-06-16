using BeerBrewery.Application.Extensions;
using BeerBrewery.Client.Components;
using BeerBrewery.Infrastructure.Data;
using BeerBrewery.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BeerBrewery.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.RegisterApplication();
            builder.Services.RegisterInfrastructure(
                builder.Configuration.GetConnectionString("DefaultConnection")!);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Auto apply migrations on startup FOR DOCKER
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var connectionString = db.Database.GetConnectionString()!;
                var dbPath = connectionString.Replace("Data Source=", "").Trim();

                if (!Path.IsPathRooted(dbPath))
                    dbPath = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, dbPath));

                var dbFolder = Path.GetDirectoryName(dbPath);
                if (!string.IsNullOrEmpty(dbFolder))
                    Directory.CreateDirectory(dbFolder);

                db.Database.Migrate();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
