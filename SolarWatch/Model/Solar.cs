namespace SolarWatch;

public class Solar
{
    public string Sunrise { get; }
    public string Sunset { get; }

    public Solar(string sunrise, string sunset)
    {
        Sunset = sunset;
        Sunrise = sunrise;
    }
}