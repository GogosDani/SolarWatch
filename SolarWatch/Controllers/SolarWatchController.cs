using Microsoft.AspNetCore.Mvc;
using SolarWatch.Exceptions;
using SolarWatch.Services;
using SolarWatch.Services.JsonParsers;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarWatchController : ControllerBase
{

    private readonly ILogger<SolarWatchController> _logger;
    private readonly ICityDataProvider _cityDataProvider;
    private readonly ICityParser _cityParser;
    private readonly ISolarInfoProvider _solarInfoProvider;
    private readonly ISolarParser _solarParser;

    public SolarWatchController(ILogger<SolarWatchController> logger, ICityDataProvider cityDataProvider, ICityParser cityParser , ISolarInfoProvider solarInfoProvider, ISolarParser solarParser)
    {
        _logger = logger;
        _cityDataProvider = cityDataProvider;
        _cityParser = cityParser;
        _solarInfoProvider = solarInfoProvider;
        _solarParser = solarParser;
    }

    [HttpGet(Name = "GetSolarWatchRoute")]
    public async Task<ActionResult<City>> GetSolarInfos(string cityName, DateOnly date)
    {
        try
        {
            var cityJson = await _cityDataProvider.GetCityData(cityName);
            var city = _cityParser.Process(cityJson);
            var solarJson = await _solarInfoProvider.GetSolarData(city.Latitude, city.Longitude, date);
            return Ok(_solarParser.Process(solarJson));
        }
        catch (CityDataException)
        {
            return NotFound("City not found");
        }
        catch (SolarDataException)
        {
            return BadRequest("Solar data could not be retrieved");
        }
        catch (Exception ex)
        {
            return NotFound("Invalid city name!");
        }
        
    }
    
    
    
    
    
    
    
    

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //         TemperatureC = Random.Shared.Next(-20, 55),
    //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     })
    //     .ToArray();
    // }
}
