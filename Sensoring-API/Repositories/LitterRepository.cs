using Microsoft.EntityFrameworkCore;       // For async database operations with Entity Framework Core
using Sensoring_API.Data;                  // Access to the database context
using Sensoring_API.Dto;                   // Data Transfer Objects for input/output models
using Sensoring_API.Models;                // The Litter entity model

namespace Sensoring_API.Repositories;

public class LitterRepository : ILitterRepository
{
    private readonly LitterDbContext _context;   // EF Core database context for accessing the database

    // Constructor injects the database context
    public LitterRepository(LitterDbContext context)
    {
        _context = context;
    }
    
    // Create a new litter record from DTO and save it to the database
    public async Task Create(LitterCreateDto litterCreateDto)
    {
        // Map DTO properties to the Litter entity
        Litter litter = new Litter
        {
            TypeOfTrash = litterCreateDto.TypeOfTrash,
            Location = litterCreateDto.Location,
            Coordinates = litterCreateDto.Coordinates,
            Time = litterCreateDto.Time
        };
        
        await _context.litters.AddAsync(litter);  // Add new entity to the DbSet asynchronously
        await _context.SaveChangesAsync();        // Persist changes to the database
    }

    // Read all litter records and convert them to Read DTOs
    public async Task<List<LitterReadDto>> Read()
    {
        // Project each Litter entity to a LitterReadDto and return as a list asynchronously
        return await _context.litters.Select(
            l => new LitterReadDto
            {
                Id = l.Id,
                TypeOfTrash = l.TypeOfTrash,
                Location = l.Location,
                Coordinates = l.Coordinates,
                Time = l.Time
            }).ToListAsync();
    }

    // Delete a litter record by its ID
    public async Task Delete(int id)
    {
        var litter = await _context.litters.FindAsync(id);  // Find entity by primary key

        if (litter == null)                                  // If not found, throw an exception
        {
            throw new KeyNotFoundException($"Litter with ID {id} not found.");
        }
        
        _context.litters.Remove(litter);                     // Remove entity from DbSet
        await _context.SaveChangesAsync();                   // Save changes to the database
    }
}