using dotenv.net;
using Microsoft.Extensions.Configuration;
using Moq;
using SolarWatch;
using SolarWatch.Data;
using SolarWatch.Services;
using SolarWatch.Services.Authentication;
using SolarWatch.Services.JsonParsers;

namespace IntegrationTest;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


public class SolarWatchWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        
        builder.ConfigureServices(services =>
        {
            // Get the added DB contexts from services
            var solarWatchDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SolarApiContext>));
            var usersDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<UsersContext>));
            if (usersDbContextDescriptor == null) throw new Exception("Couldn't find registered UserDB");
            if (solarWatchDbContextDescriptor == null) throw new Exception("Couldn't find registered SolarDB");
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
                options.UseInMemoryDatabase("UsersContextDb");
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

