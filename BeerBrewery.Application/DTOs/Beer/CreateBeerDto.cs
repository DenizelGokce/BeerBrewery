using BeerBrewery.Application.DTOs.Ingredient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeerBrewery.Application.DTOs.Beer
{
    public class CreateBeerDto
    {
        [Required(ErrorMessage = "Beer name is required.")]
        [MaxLength(100, ErrorMessage = "Beer name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Alcohol percentage is required.")]
        [Range(0, 100, ErrorMessage = "Alcohol percentage must be between 0 and 100.")]
        public double AlcoholPercentage { get; set; }
        public List<CreateIngredientDto> Ingredients { get; set; } = new();
    }
}
