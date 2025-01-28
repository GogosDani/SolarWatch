using System.Net;
using SolarWatch.Exceptions;

namespace SolarWatch.Services;

public class CityApiReader : ICityDataProvider
{
    public async Task<string> GetCityData(string cityName)
    {
        try
        {
            var url =
                $"https://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid=f322a8d4bfca8380e7b994865bd5cb42";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            throw new CityDataException("Couldn't get city data");
        }
       
    }
}