using BeerBrewery.Domain.Entities;
using BeerBrewery.Domain.Interfaces;
using BeerBrewery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Infrastructure.Repositories
{
    public class BeerRepository : EfRepository<Beer>, IBeerRepository
    {
        public BeerRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsByNameAsync(string name)
            => await DbSet.AnyAsync(b => b.Name.ToLower() == name.ToLower()
                && b.DeletedDate == null);

        public async Task<Beer?> GetByIdWithIngredientsAsync(Guid id)
            => await DbSet
                .Include(b => b.Ingredients)
                .FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);
    }

}
