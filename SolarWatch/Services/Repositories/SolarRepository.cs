using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;

namespace SolarWatch.Services.Repositories;

public class SolarRepository : ISolarRepository
{
    private SolarApiContext _context;

    public SolarRepository(SolarApiContext context)
    {
        _context = context;
    }
    
    
    public Solar? Get(DateOnly date, int cityId)
    {
        return _context.Solars.FirstOrDefault(x => x.City.Id == cityId && x.Date == date);
    }

    public void Update(Solar solar)
    {
        _context.Update(solar);
        _context.SaveChanges();
    }

    public void Add(Solar solar)
    {
        _context.Add(solar);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Remove(id);
        _context.SaveChanges();
    }
}