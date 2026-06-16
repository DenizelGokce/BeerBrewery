using BeerBrewery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Domain.Entities
{
    public class Ingredient : IEntity
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public Guid BeerId { get; set; }
        public Beer? Beer { get; set; }
    }
}
