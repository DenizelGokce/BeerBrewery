using BeerBrewery.Application.DTOs.Beer;
using BeerBrewery.Application.Extensions;
using BeerBrewery.Application.Interfaces;
using BeerBrewery.Domain.Interfaces;

namespace BeerBrewery.Application.Services;

public class BeerService : IBeerService
{
    private readonly IBeerRepository _beerRepository;

    public BeerService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<IEnumerable<BeerDto>> GetAllAsync()
    {
        var beers = await _beerRepository.GetAllAsync();
        return beers.Select(b => b.ToDto());
    }

    public async Task<BeerDto?> GetByIdAsync(Guid id)
    {
        var beer = await _beerRepository.GetByIdWithIngredientsAsync(id);
        return beer?.ToDto();
    }

    public async Task CreateAsync(CreateBeerDto dto)
    {
        // Business rule 1: alcohol percentage must be between 0 and 100
        if (dto.AlcoholPercentage < 0 || dto.AlcoholPercentage > 100)
            throw new InvalidOperationException("Alcohol percentage must be between 0 and 100.");

        // Business rule 2: beer name must be unique
        if (await _beerRepository.ExistsByNameAsync(dto.Name))
            throw new InvalidOperationException($"A beer with the name '{dto.Name}' already exists.");

        var beer = dto.ToEntity();
        await _beerRepository.AddAsync(beer);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _beerRepository.DeleteAsync(id);
    }
}