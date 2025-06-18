using Sensoring_API.Models;

namespace Sensoring_API.Dto;

/// <summary>
/// A Data Transfer Object (DTO) that represents a litter entry to be created.
/// </summary>
/// <remarks>
/// The LitterCreateDto is used for transferring data related to litter entries between the application layers.
/// This is used so that there is fine control over which data should be supplied and which shouldn't be.
/// </remarks>
public class LitterCreateDto
{
    /// <summary>
    /// This property represents the classification of litter, such as plastic, glass, metal, or organic waste.
    /// </summary>
    public required string TypeOfTrash { get; set; }
    
    /// <summary>
    ///This property specifies the textual representation of the location, such as an address or place name,
    /// where the litter event occurred.
    /// </summary>
    public required string Location { get; set; }
    
    /// <summary>
    /// This property is represented as an array of floating-point numbers, the first being latitude and the second being longitude.
    /// </summary>
    public required float[] Coordinates { get; set; }
    
    /// <summary>
    /// This property captures the exact time an instance of litter was registered and can be used
    /// for chronological ordering or filtering within the data.
    /// </summary>
    public required DateTime Time { get; set; }

    /// <summary>
    /// Creates a Litter object from the current instance of LitterCreateDto.
    /// </summary>
    /// <returns>
    /// A new instance of the Litter class populated with the data from the LitterCreateDto object.
    /// </returns>
    public Litter CreateLitter()
    {
        return new Litter
        {
            TypeOfTrash = TypeOfTrash,
            Location = Location,
            Coordinates = Coordinates,
            Time = Time
        };
    }
}