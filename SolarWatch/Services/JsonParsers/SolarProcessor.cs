using System.Text.Json;

namespace SolarWatch.Services.JsonParsers;

public class SolarProcessor : ISolarParser
{
    public Solar Process(string jsonString)
    {
        // var json = JsonDocument.Parse(jsonString);
        // var result = json.RootElement.GetProperty("results");
        // var sunset = result.GetProperty("sunset").GetString();
        // var sunrise = result.GetProperty("sunrise").GetString();
        // Solar solar = new Solar(sunrise, sunset);
        // return solar;

        throw new NotImplementedException();
    }
}