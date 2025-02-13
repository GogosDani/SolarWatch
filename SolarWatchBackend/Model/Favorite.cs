namespace SolarWatch;

public class Favorite
{
    public int Id { get; init; }
    public string UserId { get; init; }
    public int SolarId { get; init; }
    public Solar Solar { get; init; }
}