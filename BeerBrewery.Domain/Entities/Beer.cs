using BeerBrewery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Domain.Entities
{
    public class Beer : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double AlcoholPercentage { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
