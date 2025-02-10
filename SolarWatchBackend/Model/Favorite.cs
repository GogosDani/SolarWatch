namespace SolarWatch;

public class Favorite
{
    public string UserId { get; init; }
    public int SolarId { get; init; }
    public Solar Solar { get; init; }
}