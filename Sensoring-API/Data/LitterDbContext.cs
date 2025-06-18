using Microsoft.EntityFrameworkCore;
using Sensoring_API.Models;

namespace Sensoring_API.Data;

/// <summary>
/// Represents the database context for the application, providing access to the database for managing entities related to the application.
/// </summary>
/// <remarks>
/// The LitterDbContext class inherits from DbContext and is used to configure and manage database interactions.
/// It includes DbSet properties for each entity that needs to be represented in the database and overrides
/// the OnModelCreating method for additional configuration of the model.
/// </remarks>
public class LitterDbContext(DbContextOptions<LitterDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Represents litter-related data within the application, including information on the type of trash,
    /// location, geographical coordinates, and the timestamp of data collection.
    /// </summary>
    public DbSet<Litter> Litter { get; set; }

    /// <summary>
    /// Represents the API key data within the application, used for authentication and access control.
    /// </summary>
    public DbSet<ApiKey> ApiKey { get; set; }

    /// <summary>
    /// Configures the model relationships and constraints for the database schema using the <see cref="ModelBuilder"/> API.
    /// </summary>
    /// <param name="builder">An instance of <see cref="ModelBuilder"/> used to define the shape of entities, relationships, and table mappings.</param>
    /// <remarks>
    /// This method is called by the framework when the model for the context is being created. It is used to configure
    /// entity properties, relationships, keys, and constraints beyond what is discovered by convention.
    /// 
    /// In this implementation, it explicitly configures the <see cref="ApiKey"/> entity to use the <c>Key</c> property
    /// as its primary key.
    /// 
    /// Always call the base implementation to ensure that any configuration provided by the base class is applied.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApiKey>().HasKey(apiKey => apiKey.Key);
    }
}