namespace SolarWatch.Services.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAll();
    Task<City?> GetByName(string name);
    void Update(City city);
    Task<int> Add(City city);
    void Delete(int id);
}