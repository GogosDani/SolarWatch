using Microsoft.AspNetCore.Identity;
namespace SolarWatch;

public class ApplicationUser : IdentityUser
{
    public string? ProfilePicturePath { get; set; }
}