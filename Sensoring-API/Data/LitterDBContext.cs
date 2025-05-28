using Microsoft.EntityFrameworkCore;
using Sensoring_API.Models;

namespace Sensoring_API.Data;

public class LitterDbContext : DbContext
{
    public LitterDbContext(DbContextOptions<LitterDbContext> options) : base(options) { }
    public DbSet<Litter> litters { get; set; }
}