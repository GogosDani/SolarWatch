using SolarWatch.Data;

namespace SolarWatch.Services.Repositories;

public class CityRepository : ICityRepository
{
    private SolarApiContext _context;

    public CityRepository(SolarApiContext context)
    {
        _context = context;
    }
    
    
    public IEnumerable<City> GetAll()
    {
        return _context.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        return _context.Cities.FirstOrDefault(c => c.Name == name);
    }

    public void Update(City city)
    {
        _context.Update(city);
        _context.SaveChanges();
    }

    public void Add(City city)
    {
        _context.Add(city);
        _context.SaveChanges();
    }

    public void Delete(City city)
    {
        _context.Remove(city);
        _context.SaveChanges();
    }
}