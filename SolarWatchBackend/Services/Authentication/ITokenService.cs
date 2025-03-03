using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Services.Authentication;

public interface ITokenService
{
    public string CreateToken(ApplicationUser user, string role);
}