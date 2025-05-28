using Microsoft.EntityFrameworkCore;       // For Entity Framework Core functionality
using Sensoring_API.Models;                // For accessing the Litter model class

namespace Sensoring_API.Data;

public class LitterDbContext : DbContext
{
    // Constructor that accepts DbContext options and passes them to the base DbContext
    public LitterDbContext(DbContextOptions<LitterDbContext> options) : base(options) { }

    // DbSet representing the "litters" table in the database
    // Used to query and save instances of Litter entities
    public DbSet<Litter> litters { get; set; }
}