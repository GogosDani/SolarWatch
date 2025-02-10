using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;

namespace SolarWatch.Services.Repositories;

public class FavoriteRepository : IFavoriteRepository
{
    
    private SolarApiContext _context;

    public FavoriteRepository(SolarApiContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Favorite>> GetFavoritesByUserId(string userId)
    {
        return await _context.Favorites.Where(f => f.UserId == userId).Include(f => f.Solar).ToListAsync();
    }

    public async Task<int> AddFavorite(Favorite favorite)
    {
        await _context.Favorites.AddAsync(favorite);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteFavorite(int favoriteId)
    {
        Favorite favorite = await _context.Favorites.FindAsync(favoriteId);
        if (favorite == null) return 0;
        _context.Favorites.Remove(favorite);
        return await _context.SaveChangesAsync();
    }
}