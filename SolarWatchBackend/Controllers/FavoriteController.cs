using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.DTOs;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controllers;

[Route("api/favorites")]
[ApiController]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteRepository _repository;

    public FavoriteController(IFavoriteRepository repository)
    {
        _repository = repository;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetFavoritesByUserId()
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }

            var favorites = await _repository.GetFavoritesByUserId(userId);
            // Create favorite responses (without userId)
            var favoriteResponses = favorites.Select(f => new FavoriteResponse() { Id = f.Id, Solar = f.Solar });
            return Ok(favoriteResponses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{solarId}"), Authorize]
    public async Task<IActionResult> AddFavorite(int solarId)
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }
            Favorite favorite = new() { SolarId = solarId, UserId = userId };
            var favorites = await _repository.AddFavorite(favorite);
            return Ok(favorites);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{favoriteId}"), Authorize]
    public async Task<IActionResult> RemoveFromFavorite(int favoriteId)
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }
            // Get favorite to check if the user want to delete one of their favorite's id.
            var favorite = await _repository.GetFavoriteById(favoriteId);
            if (favorite.UserId != userId) return Unauthorized("You can only delete your favorites.");
            var result = await _repository.DeleteFavorite(favoriteId);
            if (result == 0) return BadRequest("Couldn't find favorite");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}