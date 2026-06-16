using BeerBrewery.Application.DTOs.Beer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Application.Interfaces
{
    public interface IBeerService
    {
        Task<IEnumerable<BeerDto>> GetAllAsync();
        Task<BeerDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateBeerDto dto);
        Task DeleteAsync(Guid id);
    }
}
