using System.Text.Json;

namespace SolarWatch.Services.JsonParsers;

public class CityProcessor : ICityParser
{
    public City Process(string jsonString)
    {
        var json = JsonDocument.Parse(jsonString);
        var lon = json.RootElement[0].GetProperty("lon").GetDouble();
        var lat = json.RootElement[0].GetProperty("lat").GetDouble();
        City currentCity = new City(lon, lat);
        return currentCity;
    }
}