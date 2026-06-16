using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBrewery.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; init; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
