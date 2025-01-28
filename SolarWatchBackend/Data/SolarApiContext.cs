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
        modelBuilder.Entity<Solar>()
            .HasOne(s => s.City)
            .WithMany()
            .HasForeignKey(s => s.CityId);
    }
}