using Microsoft.EntityFrameworkCore;
using Sensoring_API.Data;
using Sensoring_API.Dto;
using Sensoring_API.Models;
using Sensoring_API.Repositories;

public class LitterRepositoryTests
{
    private LitterDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<LitterDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new LitterDbContext(options);
    }

    [Fact]
    public async Task Create_AddsLitterToDb()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new LitterRepository(context);

        var dto = new LitterCreateDto
        {
            TypeOfTrash = "Plastic",
            Location = "Park",
            Coordinates = [1.0f, 2.0f],
            Time = DateTime.UtcNow
        };

        // Act
        await repository.Create(dto);
        var result = await context.Litter.FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Plastic", result.TypeOfTrash);
    }

    [Fact]
    public async Task Read_ReturnsListOfDtos()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        context.Litter.Add(new Litter
        {
            TypeOfTrash = "Glass",
            Location = "Beach",
            Coordinates = [3.0f, 4.0f ],
            Time = DateTime.UtcNow
        });
        await context.SaveChangesAsync();

        var repository = new LitterRepository(context);

        // Act
        var result = await repository.Read();

        // Assert
        Assert.Single(result);
        Assert.Equal("Glass", result[0].TypeOfTrash);
    }

    [Fact]
    public async Task Delete_RemovesItem()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var litter = new Litter
        {
            TypeOfTrash = "Paper",
            Location = "School",
            Coordinates = [ 5.0f, 6.0f ],
            Time = DateTime.UtcNow
        };
        context.Litter.Add(litter);
        await context.SaveChangesAsync();

        var repository = new LitterRepository(context);

        // Act
        await repository.Delete(litter.Id);

        // Assert
        var exists = await context.Litter.FindAsync(litter.Id);
        Assert.Null(exists);
    }

    [Fact]
    public async Task Delete_NonExisting_ThrowsException()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new LitterRepository(context);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.Delete(999));
    }
}
