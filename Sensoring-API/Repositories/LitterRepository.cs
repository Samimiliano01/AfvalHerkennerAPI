using Microsoft.EntityFrameworkCore;       // For async database operations with Entity Framework Core
using Sensoring_API.Data;                  // Access to the database context
using Sensoring_API.Dto;                   // Data Transfer Objects for input/output models
using Sensoring_API.Models;                // The Litter entity model

namespace Sensoring_API.Repositories;

/// <summary>
/// Provides methods to interact with the database for managing litter-related data.
/// </summary>
public class LitterRepository(LitterDbContext context) : ILitterRepository
{
    /// <summary>
    /// Creates a new litter entry and persists it to the database.
    /// </summary>
    /// <param name="litterCreateDto">An object containing the details of the litter to be created, including type of trash, location, coordinates, and time.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Create(LitterCreateDto litterCreateDto)
    {
        // Save the entity to the database
        await context.Litter.AddAsync(litterCreateDto.CreateLitter());
        await context.SaveChangesAsync();
    }


    /// <summary>
    /// Retrieves all litter entries from the database and projects them into a list of LitterReadDto objects.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains a list of LitterReadDto objects, each representing a litter entry with details such as type of trash, location, coordinates, and time.</returns>
    public async Task<List<LitterReadDto>> Read()
    {
        // Project each Litter entity to a LitterReadDto and return as a list asynchronously
        return await context.Litter.Select(litter => new LitterReadDto(litter)).ToListAsync();
    }


    /// <summary>
    /// Deletes a litter entry from the database based on the provided ID.
    /// </summary>
    /// <param name="id">The unique identifier of the litter entry to be deleted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Delete(int id)
    {
        // Find the litter entry by id
        var litter = await context.Litter.FindAsync(id);

        // The key must be present
        if (litter == null)
        {
            throw new KeyNotFoundException($"Litter with ID {id} not found.");
        }
        
        // Removes the entity from the database
        context.Litter.Remove(litter);
        await context.SaveChangesAsync();
    }
}