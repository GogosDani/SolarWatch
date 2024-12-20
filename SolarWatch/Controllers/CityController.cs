using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controllers;

public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    [HttpPost("City"), Authorize(Roles = "admin")]
    public async Task<ActionResult> Post([FromBody] City city)
    {
        try
        {
            var id = await _cityRepository.Add(city);
            return Ok(id);
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
            var deletedId = await _cityRepository.Delete(id);
            return Ok(deletedId);
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
            var id = await _cityRepository.Update(city);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}