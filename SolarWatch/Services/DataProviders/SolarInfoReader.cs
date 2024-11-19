using System.Net;
using SolarWatch.Exceptions;

namespace SolarWatch.Services;

public class SolarInfoReader : ISolarInfoProvider
{
    public async Task<string> GetSolarData(double lat, double lon, DateOnly date)
    {
        try
        {
            var convertedDate = date.ToString("yyyy-MM-dd");
            var solarApi = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={convertedDate}";
            using var client = new HttpClient();
            var response = await client.GetAsync(solarApi);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            throw new SolarDataException("Couldn't get solar datas");
        }
    }
}