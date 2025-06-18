using Sensoring_API.Dto;

namespace Sensoring_API.Repositories;

/// <summary>
/// Defines a contract for managing litter-related data in a repository.
/// </summary>
public interface ILitterRepository
{
    /// <summary>
    /// Creates a new litter entry in the repository.
    /// </summary>
    /// <param name="litterCreateDto">
    /// An object containing the details of the litter entry to be added. This includes:
    /// - Type of trash
    /// - Location
    /// - Coordinates
    /// - Time
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation of creating the litter entry.
    /// </returns>
    Task Create(LitterCreateDto litterCreateDto);

    /// <summary>
    /// Retrieves all litter entries from the repository.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result contains a list of LitterReadDto objects, each containing the details of a litter entry:
    /// - Identifier
    /// - Type of trash
    /// - Location
    /// - Coordinates
    /// - Time.
    /// </returns>
    Task<List<LitterReadDto>> Read();

    /// <summary>
    /// Deletes a litter entry from the repository based on the provided ID.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the litter entry to be deleted.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task Delete(int id);
}