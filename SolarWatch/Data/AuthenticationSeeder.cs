using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Data;

public class AuthenticationSeeder
{
    private RoleManager<IdentityRole> roleManager;
    private UserManager<IdentityUser> userManager;

    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }
    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(roleManager);
        tUser.Wait();
    }
    
    public void AddAdmin(string name, string email, string password)
    {
        var tAdmin = CreateAdminIfNotExists(name ,email, password);
        tAdmin.Wait();
    }

    private async Task CreateAdminIfNotExists(string name, string email, string password)
    {
        var adminInDb = await userManager.FindByEmailAsync(email);
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = name, Email = email };
            var adminCreated = await userManager.CreateAsync(admin,password);

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
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