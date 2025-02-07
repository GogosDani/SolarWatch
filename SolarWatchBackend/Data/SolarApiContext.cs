using Microsoft.EntityFrameworkCore;

namespace SolarWatch.Data;

public class SolarApiContext : DbContext
{
    public SolarApiContext(DbContextOptions<SolarApiContext> options) 
        : base(options)
    {
    }
    
    public DbSet<City> Cities { get; set; }
    public DbSet<Solar> Solars { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasData(
                new City(){Id = 1, Latitude = 40.51, Longitude = 2.19, Name = "Barcelona"}, 
                new City(){Id = 2, Latitude = 48.85, Longitude = 2.35, Name = "Paris"},
                new City(){Id = 3, Latitude = 51.51, Longitude = -0.13, Name = "London"},
                new City(){Id = 4, Latitude = 40.71, Longitude = -74.01, Name = "New York"},
                new City(){Id = 5, Latitude = 34.05, Longitude = -118.24, Name = "Los Angeles"},
                new City(){Id = 6, Latitude = 41.90, Longitude = 12.49, Name = "Rome"},
                new City(){Id = 7, Latitude = 35.68, Longitude = 139.69, Name = "Tokyo"},
                new City(){Id = 8, Latitude = 55.75, Longitude = 37.62, Name = "Moscow"},
                new City(){Id = 9, Latitude = 52.52, Longitude = 13.40, Name = "Berlin"},
                new City(){Id = 10, Latitude = 37.77, Longitude = -122.42, Name = "San Francisco"},
                new City(){Id = 11, Latitude = 39.90, Longitude = 116.40, Name = "Beijing"},
                new City(){Id = 12, Latitude = 19.43, Longitude = -99.13, Name = "Mexico City"},
                new City(){Id = 13, Latitude = 28.61, Longitude = 77.23, Name = "New Delhi"},
                new City(){Id = 14, Latitude = -33.87, Longitude = 151.21, Name = "Sydney"},
                new City(){Id = 15, Latitude = 1.35, Longitude = 103.82, Name = "Singapore"},
                new City(){Id = 16, Latitude = 50.08, Longitude = 14.43, Name = "Prague"},
                new City(){Id = 17, Latitude = 59.33, Longitude = 18.06, Name = "Stockholm"},
                new City(){Id = 18, Latitude = 31.23, Longitude = 121.47, Name = "Shanghai"},
                new City(){Id = 19, Latitude = -23.55, Longitude = -46.63, Name = "São Paulo"},
                new City(){Id = 20, Latitude = 6.52, Longitude = 3.37, Name = "Lagos"},
                new City(){Id = 21, Latitude = 43.65, Longitude = -79.38, Name = "Toronto"},
                new City(){Id = 22, Latitude = 55.68, Longitude = 12.57, Name = "Copenhagen"},
                new City(){Id = 23, Latitude = 25.28, Longitude = 51.53, Name = "Doha"},
                new City(){Id = 24, Latitude = -26.20, Longitude = 28.04, Name = "Johannesburg"},
                new City(){Id = 25, Latitude = 35.69, Longitude = 51.42, Name = "Tehran"}
            );
        });
            
        
        modelBuilder.Entity<Solar>(entity =>
        {
            entity.HasOne(s => s.City)
                .WithMany()
                .HasForeignKey(s => s.CityId);

            entity.HasData(
                new Solar(){Id = 1, Sunrise = "06:30", Sunset = "18:45", Date = new DateOnly(2024, 2, 7), CityId = 1},
                new Solar(){Id = 2, Sunrise = "07:15", Sunset = "19:05", Date = new DateOnly(2024, 2, 7), CityId = 2},
                new Solar(){Id = 3, Sunrise = "07:45", Sunset = "18:30", Date = new DateOnly(2024, 2, 7), CityId = 3},
                new Solar(){Id = 4, Sunrise = "06:50", Sunset = "17:55", Date = new DateOnly(2024, 2, 7), CityId = 4},
                new Solar(){Id = 5, Sunrise = "06:55", Sunset = "18:20", Date = new DateOnly(2024, 2, 7), CityId = 5},
                new Solar(){Id = 6, Sunrise = "07:10", Sunset = "18:40", Date = new DateOnly(2024, 2, 7), CityId = 6},
                new Solar(){Id = 7, Sunrise = "06:20", Sunset = "17:30", Date = new DateOnly(2024, 2, 7), CityId = 7},
                new Solar(){Id = 8, Sunrise = "08:15", Sunset = "16:45", Date = new DateOnly(2024, 2, 7), CityId = 8},
                new Solar(){Id = 9, Sunrise = "07:50", Sunset = "17:20", Date = new DateOnly(2024, 2, 7), CityId = 9},
                new Solar(){Id = 10, Sunrise = "06:40", Sunset = "17:50", Date = new DateOnly(2024, 2, 7), CityId = 10}
            );
        });
            
        
    }
}