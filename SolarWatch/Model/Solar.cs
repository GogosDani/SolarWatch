namespace SolarWatch;

public class Solar
{
    public int Id { get; init; }
    public string Sunrise { get; init; }
    public string Sunset { get; init; }
    public DateOnly Date { get; init; }
    public int CityId { get; init; }
    public City City { get; init; }
}