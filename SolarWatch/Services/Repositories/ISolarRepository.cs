namespace SolarWatch.Services.Repositories;

public interface ISolarRepository
{
    Task<Solar?> Get(DateOnly date, int cityId);
    Task<int> Update(Solar solar);
    Task<int> Add(Solar solar);
    Task<int> Delete(int id);
}