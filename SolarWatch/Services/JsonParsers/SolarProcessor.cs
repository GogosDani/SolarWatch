using System.Text.Json;

namespace SolarWatch.Services.JsonParsers;

public class SolarProcessor : ISolarParser
{
    public Solar Process(string jsonString, City city, DateOnly date)
    {
        var json = JsonDocument.Parse(jsonString);
        var result = json.RootElement.GetProperty("results");
        var sunset = result.GetProperty("sunset").GetString();
        var sunrise = result.GetProperty("sunrise").GetString();
        Solar solar = new Solar() {City = city, CityId = city.Id, Sunset = sunset, Sunrise = sunrise, Date = date};
        return solar;
    }
}