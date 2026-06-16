using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Application.DTOs.Ingredient
{
    public class IngredientDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
    }
}
