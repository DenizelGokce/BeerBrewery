using BeerBrewery.Application.DTOs.Beer;
using BeerBrewery.Domain.Entities;

namespace BeerBrewery.Application.Extensions
{
    public static class BeerExtensions
    {
        public static BeerDto ToDto(this Beer beer)
            => new()
            {
                Id = beer.Id,
                Name = beer.Name,
                Description = beer.Description,
                AlcoholPercentage = beer.AlcoholPercentage,
                Ingredients = beer.Ingredients.Select(i => i.ToDto()).ToList()
            };

        public static Beer ToEntity(this CreateBeerDto dto)
            => new()
            {
                Name = dto.Name,
                Description = dto.Description,
                AlcoholPercentage = dto.AlcoholPercentage,
                Ingredients = dto.Ingredients.Select(i => i.ToEntity()).ToList()
            };
    }
}
