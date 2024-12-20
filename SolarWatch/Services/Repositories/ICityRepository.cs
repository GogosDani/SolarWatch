namespace SolarWatch.Services.Repositories;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAll();
    Task<City?> GetByName(string name);
    Task<int> Update(City city);
    Task<int> Add(City city);
    Task<int> Delete(int id);
    Task<IEnumerable<City>> GetByPage(int pageNumber);
    Task<City> GetById(int id);
}