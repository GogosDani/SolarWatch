namespace SolarWatch.Services.Repositories;

public interface ISolarRepository
{
    Solar? Get(DateOnly date, int cityId);
    void Update(Solar solar);
    void Add(Solar solar);
    void Delete(int id);
}