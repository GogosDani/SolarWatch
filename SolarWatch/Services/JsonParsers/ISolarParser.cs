namespace SolarWatch.Services.JsonParsers;

public interface ISolarParser
{
    Solar Process(string jsonString);
}