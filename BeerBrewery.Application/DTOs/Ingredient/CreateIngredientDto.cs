using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Application.DTOs.Ingredient
{
    public class CreateIngredientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
    }
}
