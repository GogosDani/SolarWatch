using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;

namespace SolarWatch.Services.Repositories;

public class CityRepository : ICityRepository
{
    private SolarApiContext _context;

    public CityRepository(SolarApiContext context)
    {
        _context = context;
    }
    
    
    public async Task<IEnumerable<City>> GetAll()
    {
        return await _context.Cities.ToListAsync();
    }

    public async Task<City?> GetByName(string name)
    {
        return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async void Update(City city)
    {
        _context.Cities.Update(city);
       await _context.SaveChangesAsync();
    }

    public async Task<int> Add(City city)
    {
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
        return city.Id;
    }

    public async void Delete(int id)
    {
        _context.Cities.Remove(_context.Cities.FirstOrDefault(c => c.Id == id));
        await _context.SaveChangesAsync();
    }
}