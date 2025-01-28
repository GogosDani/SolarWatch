namespace SolarWatch.Services.JsonParsers;

public interface ICityParser
{
    City Process(string jsonString);
}