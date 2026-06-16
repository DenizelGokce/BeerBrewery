using BeerBrewery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Beer> Beers => Set<Beer>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beer>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Description).HasMaxLength(500);
                entity.HasMany(b => b.Ingredients)
                      .WithOne(i => i.Beer)
                      .HasForeignKey(i => i.BeerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Name).IsRequired().HasMaxLength(100);
                entity.Property(i => i.Type).IsRequired().HasMaxLength(50);
                entity.Property(i => i.Quantity).IsRequired().HasMaxLength(50);
            });
        }
    }
}
