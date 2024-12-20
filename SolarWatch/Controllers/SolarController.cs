using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controllers;

[Route("api/solar")]
[ApiController]
public class SolarController : ControllerBase
{
    
    private readonly ISolarRepository _solarRepository;

    public SolarController(ISolarRepository repository)
    {
        _solarRepository = repository;
    }
    
    [HttpPost("SolarInfo"), Authorize(Roles = "admin")]
    public async Task<ActionResult> Post([FromBody] Solar solar)
    {
        try
        {
            var id = await _solarRepository.Add(solar);
            return Ok(id);
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
            var deletedId = await _solarRepository.Delete(id);
            return Ok(deletedId);
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
            var id = await _solarRepository.Update(solar);
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
            var solars = await _solarRepository.GetByPage(pageNumber);
            return Ok(solars);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}