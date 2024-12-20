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
    
    
    public async Task<Solar?> Get(DateOnly date, int cityId)
    {
        return await _context.Solars.FirstOrDefaultAsync(x => x.City.Id == cityId && x.Date == date);
    }

    public async Task<int> Update(Solar solar)
    {
        _context.Solars.Update(solar);
        await _context.SaveChangesAsync();
        return solar.Id;
    }

    public async Task<int> Add(Solar solar)
    {
        _context.Solars.Add(solar);
        await _context.SaveChangesAsync();
        return solar.Id;
    }

    public async Task<int> Delete(int id)
    {
        _context.Solars.Remove(await _context.Solars.FirstOrDefaultAsync(s => s.Id == id));
        await _context.SaveChangesAsync();
        return id;
    }

    public async Task<IEnumerable<Solar>> GetByPage(int pageNumber)
    {
        return await _context.Solars.OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * 10)
            .Take(10)
            .ToListAsync();
    }
}