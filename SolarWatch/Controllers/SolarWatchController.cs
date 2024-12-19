using Microsoft.AspNetCore.Authorization;
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

    [HttpGet(Name = "GetSolarWatchRoute"), Authorize(Roles = "User, Admin")]
    public async Task<ActionResult<City>> GetSolarInfos(string cityName, DateOnly date)
    {
        try
        {
            var city = await _cityRepository.GetByName(cityName);
            Solar solarData = null;
            // city can be null now, if there isn't any data for that city in the DB
            if (city == null)
            {
                // Get the city data from the API
                var cityJson = await _cityDataProvider.GetCityData(cityName);
                city =  _cityParser.Process(cityJson);
                var cityId = _cityRepository.Add(city);
                // If we added the city to the DB just now, we should get the solar data from te API too.
                solarData = await GetSolarDataFromApi(city, date);
            }
            // If we got the city from DB
            else
            {
                // Try to get the solar data from the DB too.
                solarData = await _solarRepository.Get(date, city.Id);
                if (solarData == null)
                {
                    solarData = await GetSolarDataFromApi(city, date);
                }
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

    [HttpPost("City"), Authorize(Roles = "admin")]
    public async Task<ActionResult> Post([FromBody] City city)
    {
        try
        {
            _cityRepository.Add(city);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("SolarInfo"), Authorize(Roles = "admin")]
    public async Task<ActionResult> Post([FromBody] Solar solar)
    {
        try
        {
            _solarRepository.Add(solar);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("SolarInfo"), Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteSolar(int id)
    {
        try
        {
            _solarRepository.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("City"), Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteCity(int id)
    {
        try
        {
            _cityRepository.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("SolarInfo"), Authorize(Roles = "admin")]
    public async Task<ActionResult> EditSolar([FromBody] Solar solar)
    {
        try
        {
            _solarRepository.Update(solar);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("City"), Authorize(Roles = "admin")]
    public async Task<ActionResult> EditCity([FromBody] City city)
    {
        try
        {
            _cityRepository.Update(city);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
    
    private async Task<Solar> GetSolarDataFromApi(City city, DateOnly date)
    {
        var solarJson = await _solarInfoProvider.GetSolarData(city.Latitude, city.Longitude, date);
        var parsedSolar = _solarParser.Process(solarJson, city, date);
        _solarRepository.Add(parsedSolar);
        return parsedSolar;
    }
}


