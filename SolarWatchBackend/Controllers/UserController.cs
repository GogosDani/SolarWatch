using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SolarWatch.DTOs;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetUserInfos()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId not found.");
        }

        var userInfos = await _repository.GetUserById(userId);
        return Ok(userInfos);
    }

    [HttpPatch]
    public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordRequest model)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest("UserId not found.");
        }

        var success = await _repository.ChangePassword(model.CurrentPassword, model.NewPassword, userId);
        if (!success)
        {
            return BadRequest("Failed to change password.");
        }

        return Ok("Password changed successfully.");
    }
}