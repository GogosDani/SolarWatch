using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using SolarWatch.DTOs;

namespace SolarWatch.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserResponse> GetUserById(string id)
    {
        var user =  await _userManager.FindByIdAsync(id);
        if(user == null) throw new InvalidOperationException("User not found");
        return new UserResponse(){Email = user.Email, UserName = user.UserName, ProfilePictureUrl = user.ProfilePictureUrl};
    }

    public async Task<bool> ChangePassword(string currentPassword, string newPassword, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if(user == null) throw new InvalidOperationException("User not found");
        var result =  await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }

    public async Task<bool> EditProfilePicture(string userId, string url)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)return false; 
        user.ProfilePictureUrl = url;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}