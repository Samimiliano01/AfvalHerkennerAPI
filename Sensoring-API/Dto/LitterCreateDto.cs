namespace Sensoring_API.Dto;

public class LitterCreateDto
{
    public required string Category { get; set; }
    public required string Location { get; set; }
    public required float[] Coordinates { get; set; }
    public required DateTime Time { get; set; } 
}