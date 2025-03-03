using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using SolarWatch.DTOs;
using SolarWatch.Services.ProfilePicture;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IS3Service _service;

    public UserController(IUserRepository repository, IS3Service service)
    {
        _repository = repository;
        _service = service;
    }

    [HttpGet, Authorize]
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

    [HttpPatch, Authorize]
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

    [HttpPost("/api/profile-picture"), Authorize]
    public async Task<IActionResult> UploadProfilePicture(IFormFile picture)
    {
        try
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId not found.");
            }
            var fileExtension = Path.GetExtension(picture.FileName);
            var fileName = $"{userId}_profile{fileExtension}";
            using var fileStream = picture.OpenReadStream();
            var fileUrl = await _service.UploadFileAsync(fileStream, fileName);
            var succeed = await _repository.EditProfilePicture(userId, fileUrl);
            if (succeed) return Ok();
            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}