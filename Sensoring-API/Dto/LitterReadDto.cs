namespace Sensoring_API.Dto;

public class LitterReadDto
{
    public required int Id { get; set; }
    public required string TypeOfTrash { get; set; }
    public required string Location { get; set; }
    public required string Coordinates { get; set; }
    public required DateTime Time { get; set; } 
}