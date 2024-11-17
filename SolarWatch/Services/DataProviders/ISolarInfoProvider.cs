namespace SolarWatch.Services;

public interface ISolarInfoProvider
{
    Task<string> GetSolarData(double lat, double lon, DateOnly date);
}