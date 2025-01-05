using SolarWatch.Data;

namespace IntegrationTest;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;


public class SolarWatchWebApplicationFactory : WebApplicationFactory<SolarWatch.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Get the added DB contexts from services
            var solarWatchDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SolarApiContext>));
            var usersDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsersContext>));
            
            // Remove these DB contexts (used in real app)
            services.Remove(solarWatchDbContextDescriptor);
            services.Remove(usersDbContextDescriptor);

            // Add in-memory DB contexts for tests
            services.AddDbContext<SolarApiContext>(options =>
            {
                options.UseInMemoryDatabase("SolarApiTestDb");
            });
            services.AddDbContext<UsersContext>(options =>
            {
                options.UseInMemoryDatabase("UsersContextTestDb");
            });

            // Initialize in-memory DBs
            using var scope = services.BuildServiceProvider().CreateScope();
            var solarContext = scope.ServiceProvider.GetRequiredService<SolarApiContext>();
            solarContext.Database.EnsureDeleted();
            solarContext.Database.EnsureCreated();
            var userContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
            userContext.Database.EnsureDeleted();
            userContext.Database.EnsureCreated();
        });
    }
}
