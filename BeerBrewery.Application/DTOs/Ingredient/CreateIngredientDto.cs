using BeerBrewery.Application.DTOs.Ingredient.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerBrewery.Application.DTOs.Ingredient
{
    public class CreateIngredientDto
    {
        [Required(ErrorMessage = "Ingredient name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingredient type is required.")]
        public IngredientType Type { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public string Quantity { get; set; } = string.Empty;
    }
}
