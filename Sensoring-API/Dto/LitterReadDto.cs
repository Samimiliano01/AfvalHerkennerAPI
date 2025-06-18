using Sensoring_API.Models;

namespace Sensoring_API.Dto;

/// <summary>
/// A Data Transfer Object (DTO) that represents a litter entry to be read.
/// </summary>
/// <remarks>
/// The LitterReadDto is used for transferring data related to litter entries between the application layers.
/// This is used so that there is fine control over which data should be supplied and which shouldn't be.
/// </remarks>
public class LitterReadDto
{
    /// <summary>
    /// A constructor creating the LitterReadDto from a Litter model.
    /// </summary>
    /// <param name="model">The litter model to create the DTO from.</param>
    public LitterReadDto(Litter model)
    {
        Id = model.Id;
        TypeOfTrash = model.TypeOfTrash;
        Location = model.Location;;
        Coordinates = model.Coordinates;;
        Time = model.Time;;
    }

    /// <summary>
    /// This property represents the primary key for a litter entry.
    /// It is used to uniquely identify each litter record in the system.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// This property represents the classification of litter, such as plastic, glass, metal, or organic waste.
    /// </summary>
    public string TypeOfTrash { get; private set; }
    
    /// <summary>
    ///This property specifies the textual representation of the location, such as an address or place name,
    /// where the litter event occurred.
    /// </summary>
    public string Location { get; private set; }
    
    /// <summary>
    /// This property is represented as an array of floating-point numbers, the first being latitude and the second being longitude.
    /// </summary>
    public float[] Coordinates { get; private set; }
    
    /// <summary>
    /// This property captures the exact time an instance of litter was registered and can be used
    /// for chronological ordering or filtering within the data.
    /// </summary>
    public DateTime Time { get; private set; }
}