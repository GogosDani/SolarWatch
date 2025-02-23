using SolarWatch.DTOs;

namespace SolarWatch.Services.Repositories;

public interface IUserRepository
{
    public Task<UserResponse> GetUserById(string id);
    public Task<bool> ChangePassword(string currentPassword, string newPassword, string userId);
    public Task UpdateProfilePicture(string userId, string fileName);
}