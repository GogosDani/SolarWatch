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
        _context.Cities.Update(city);
        _context.SaveChanges();
    }

    public int Add(City city)
    {
        _context.Cities.Add(city);
        _context.SaveChanges();
        return city.Id;
    }

    public void Delete(int id)
    {
        _context.Cities.Remove(_context.Cities.FirstOrDefault(c => c.Id == id));
        _context.SaveChanges();
    }
}