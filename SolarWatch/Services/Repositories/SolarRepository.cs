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
    
    
    public Task<Solar?> Get(DateOnly date, int cityId)
    {
        return _context.Solars.FirstOrDefaultAsync(x => x.City.Id == cityId && x.Date == date);
    }

    public async void Update(Solar solar)
    {
        _context.Solars.Update(solar);
        _context.SaveChangesAsync();
    }

    public async void Add(Solar solar)
    {
        _context.Solars.Add(solar);
        _context.SaveChangesAsync();
    }

    public async void Delete(int id)
    {
        _context.Solars.Remove(_context.Solars.FirstOrDefault(s => s.Id == id));
        _context.SaveChangesAsync();
    }
}