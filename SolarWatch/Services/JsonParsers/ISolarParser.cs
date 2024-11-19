using System.Runtime.InteropServices.JavaScript;

namespace SolarWatch.Services.JsonParsers;

public interface ISolarParser
{
    Solar Process(string jsonString, City city, DateOnly date);
}