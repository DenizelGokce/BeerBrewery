using BeerBrewery.Application.DTOs.Ingredient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Application.DTOs.Beer
{
    public class BeerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double AlcoholPercentage { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
