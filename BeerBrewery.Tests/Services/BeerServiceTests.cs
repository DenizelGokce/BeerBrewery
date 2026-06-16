using BeerBrewery.Application.DTOs.Beer;
using BeerBrewery.Application.DTOs.Ingredient;
using BeerBrewery.Application.Services;
using BeerBrewery.Domain.Entities;
using BeerBrewery.Domain.Interfaces;
using Moq;

namespace BeerBrewery.Tests.Services;

public class BeerServiceTests
{
    private readonly Mock<IBeerRepository> _mockRepo;
    private readonly BeerService _service;

    public BeerServiceTests()
    {
        _mockRepo = new Mock<IBeerRepository>();
        _service = new BeerService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenAlcoholPercentageIsNegative()
    {
        // Arrange
        var dto = new CreateBeerDto
        {
            Name = "Test Beer",
            AlcoholPercentage = -1
        };

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenAlcoholPercentageExceeds100()
    {
        // Arrange
        var dto = new CreateBeerDto
        {
            Name = "Test Beer",
            AlcoholPercentage = 101
        };

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenBeerNameAlreadyExists()
    {
        // Arrange
        var dto = new CreateBeerDto
        {
            Name = "Duplicate Beer",
            AlcoholPercentage = 5
        };

        _mockRepo.Setup(r => r.ExistsByNameAsync("Duplicate Beer"))
                 .ReturnsAsync(true);

        // Act
        var act = async () => await _service.CreateAsync(dto);

        // Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    [Fact]
    public async Task CreateAsync_ShouldSucceed_WhenBeerIsValid()
    {
        // Arrange
        var dto = new CreateBeerDto
        {
            Name = "Valid Beer",
            AlcoholPercentage = 5,
            Ingredients = new List<CreateIngredientDto>
        {
            new() { Name = "Hop", Type = "Hop", Quantity = "100g" }
        }
        };

        _mockRepo.Setup(r => r.ExistsByNameAsync("Valid Beer"))
                 .ReturnsAsync(false);

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Beer>()))
                 .Returns(Task.CompletedTask);

        // Act
        await _service.CreateAsync(dto);

        // Assert
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Beer>()), Times.Once);
    }
}