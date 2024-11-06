namespace SolarWatch.Services;

public interface ISolarInfoProvider
{
    string GetSolarData(double lat, double lon, DateOnly date);
}