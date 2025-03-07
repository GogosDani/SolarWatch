using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Data;

public class AuthenticationSeeder
{
    private RoleManager<IdentityRole> roleManager;

    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        this.roleManager = roleManager;
    }
    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(roleManager);
        tUser.Wait();
    }

    private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin")); 
    }

    async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
}