using BeerBrewery.Domain.Interfaces;
using BeerBrewery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<T> DbSet;

        public EfRepository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await DbSet.Where(e => e.DeletedDate == null).ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id)
            => await DbSet.FirstOrDefaultAsync(e => e.Id == id && e.DeletedDate == null);

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null) return;

            entity.DeletedDate = DateTime.UtcNow;
            await Context.SaveChangesAsync();
        }
    }

}
