namespace SolarWatch.Services;

public interface ICityDataProvider
{
    string GetCityData(string cityName);
}