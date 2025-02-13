namespace SolarWatch.Services.Repositories;

public interface IFavoriteRepository
{
    public Task<IEnumerable<Favorite>> GetFavoritesByUserId(string userId);
    public Task<int> AddFavorite(Favorite favorite);
    public Task<int> DeleteFavorite(int favoriteId);
    public Task<Favorite> GetFavoriteById(int id);
}