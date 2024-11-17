namespace SolarWatch;

public class City
{
    public double Longitude { get; }
    public double Latitude { get; }

    public City(double longitude, double latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }
}