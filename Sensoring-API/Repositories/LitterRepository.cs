using Microsoft.EntityFrameworkCore;
using Sensoring_API.Data;
using Sensoring_API.Dto;
using Sensoring_API.Models;

namespace Sensoring_API.Repositories;

public class LitterRepository : ILitterRepository
{
    private readonly LitterDbContext _context;

    public LitterRepository(LitterDbContext context)
    {
        _context = context;
    }
    
    public async Task Create(LitterCreateDto litterCreateDto)
    {
        Litter litter = new Litter
        {
            TypeOfTrash = litterCreateDto.TypeOfTrash,
            Location = litterCreateDto.Location,
            Coordinates = litterCreateDto.Coordinates,
            Time = litterCreateDto.Time
        };
        
        await _context.litters.AddAsync(litter);
        await _context.SaveChangesAsync();
    }

    public async Task<List<LitterReadDto>> Read()
    {
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

    public async Task Delete(int id)
    {
        var litter = await _context.litters.FindAsync(id);
        if (litter != null)
        {
            _context.litters.Remove(litter);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Litter with id {id} not found.");
        }
    }
}