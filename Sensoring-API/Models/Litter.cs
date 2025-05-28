namespace Sensoring_API.Models;

public class Litter
{
    public int Id { get; set; }
    public required string TypeOfTrash { get; set; }
    public required string Location { get; set; }
    public required string Coordinates { get; set; }
    public required DateTime Time { get; set; } 
}