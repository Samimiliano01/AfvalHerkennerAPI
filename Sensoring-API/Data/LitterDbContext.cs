using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;       // For Entity Framework Core functionality
using Sensoring_API.Models;                // For accessing the Litter model class

namespace Sensoring_API.Data;

public class LitterDbContext(DbContextOptions<LitterDbContext> options) : IdentityDbContext(options)
{
    // Constructor that accepts DbContext options and passes them to the base DbContext

    // DbSet representing the "litters" table in the database
    // Used to query and save instances of Litter entities
    public DbSet<Litter> Litter { get; set; }
}