namespace SolarWatch.Services.Repositories;

public interface ICityRepository
{
    IEnumerable<City> GetAll();
    City? GetByName(string name);
    void Update(City city);
    int Add(City city);
    void Delete(int id);
}