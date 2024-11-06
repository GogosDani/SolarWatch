using System.Net;

namespace SolarWatch.Services;

public class CityApiReader : ICityDataProvider
{
    public string GetCityData(string cityName)
    {
        var url =
            $"https://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid=f322a8d4bfca8380e7b994865bd5cb42";
        using var client = new WebClient();
        return client.DownloadString(url);
    }
}