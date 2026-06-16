using BeerBrewery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Domain.Interfaces
{
    public interface IBeerRepository : IRepository<Beer>
    {
        Task<bool> ExistsByNameAsync(string name);
        Task<Beer?> GetByIdWithIngredientsAsync(Guid id);
    }
}
