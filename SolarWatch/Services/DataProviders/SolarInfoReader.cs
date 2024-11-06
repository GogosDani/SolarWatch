using System.Net;

namespace SolarWatch.Services;

public class SolarInfoReader : ISolarInfoProvider
{
    public string GetSolarData(double lat, double lon, DateOnly date)
    {
        try
        {
            var convertedDate = date.ToString("yyyy-MM-dd");
            var solarApi = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}&date={convertedDate}";
            using var client = new WebClient();
            return client.DownloadString(solarApi);
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting solar data", ex);
        }
      
    }
}