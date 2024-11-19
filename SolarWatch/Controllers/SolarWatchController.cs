using Microsoft.AspNetCore.Mvc;
using SolarWatch.Exceptions;
using SolarWatch.Services;
using SolarWatch.Services.JsonParsers;
using SolarWatch.Services.Repositories;

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
    private readonly ICityRepository _cityRepository;
    private readonly ISolarRepository _solarRepository;

    public SolarWatchController(ILogger<SolarWatchController> logger, ICityDataProvider cityDataProvider, ICityParser cityParser , ISolarInfoProvider solarInfoProvider, ISolarParser solarParser, ISolarRepository solarRepository, ICityRepository cityRepository)
    {
        _logger = logger;
        _cityDataProvider = cityDataProvider;
        _cityParser = cityParser;
        _solarInfoProvider = solarInfoProvider;
        _solarParser = solarParser;
        _cityRepository = cityRepository;
        _solarRepository = solarRepository;
    }

    [HttpGet(Name = "GetSolarWatchRoute")]
    public async Task<ActionResult<City>> GetSolarInfos(string cityName, DateOnly date)
    {
        try
        {
            var city = _cityRepository.GetByName(cityName);
            // city can be null now, if there isn't any data for that city in the DB
            if (city == null)
            {
                // Console.WriteLine("Used foreign API for city");
                var cityJson = await _cityDataProvider.GetCityData(cityName);
                city = _cityParser.Process(cityJson);
                _cityRepository.Add(city);
                city = _cityRepository.GetByName(cityName);
            }
    
            var solarData = _solarRepository.Get(date, city.Id);
            // solarData can be null now, if there isn't any data for that info in the DB
            if (solarData == null)
            {
                // Console.WriteLine("Used foreign API for solar");
                var solarJson = await _solarInfoProvider.GetSolarData(city.Latitude, city.Longitude, date);
                var parsedSolar = _solarParser.Process(solarJson, city, date);
                _solarRepository.Add(parsedSolar);
                solarData = _solarRepository.Get(date, city.Id);
            }
           
            return Ok(solarData);
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
            return NotFound(ex.Message);
        }
        
    }
    
}
