using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controllers;

[Route("api/city")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    [HttpPost(), Authorize(Roles = "admin")]
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
    
    [HttpDelete("{id}"), Authorize(Roles = "admin")]
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
    
    [HttpPut(), Authorize(Roles = "admin")]
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

    [HttpGet("{pageNumber}"), Authorize(Roles = "admin")] 
    public async Task<ActionResult> GetByPage(int pageNumber)
    {
        try
        {
            var cities = await _cityRepository.GetByPage(pageNumber);
            return Ok(cities);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
}