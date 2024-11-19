using System.Text.Json;

namespace SolarWatch.Services.JsonParsers;

public class CityProcessor : ICityParser
{
     public City Process(string jsonString)
     {
    var json = JsonDocument.Parse(jsonString);
    var lon = json.RootElement[0].GetProperty("lon").GetDouble();
    var lat = json.RootElement[0].GetProperty("lat").GetDouble();
    var name = json.RootElement[0].GetProperty("name").GetString();
    City currentCity = new City() {Longitude = lon, Latitude = lat, Name = name};
    return currentCity;
     }
}