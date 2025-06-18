namespace Sensoring_API.Models;

/// <summary>
/// Represents litter-related data including trash type, location, coordinates, and timestamp.
/// </summary>
public class Litter
{
    /// <summary>
    /// This property is used as the primary key in the database for the Litter table.
    /// </summary>
    public int Id { get; set; }

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
}