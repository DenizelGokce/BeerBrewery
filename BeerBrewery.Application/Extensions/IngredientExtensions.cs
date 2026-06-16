using BeerBrewery.Application.DTOs.Ingredient;
using BeerBrewery.Domain.Entities;

namespace BeerBrewery.Application.Extensions
{
    public static class IngredientExtensions
    {
        public static IngredientDto ToDto(this Ingredient ingredient)
            => new()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Type = ingredient.Type,
                Quantity = ingredient.Quantity
            };

        public static Ingredient ToEntity(this CreateIngredientDto dto)
            => new()
            {
                Name = dto.Name,
                Type = dto.Type,
                Quantity = dto.Quantity
            };
    }
}