namespace SolarWatch.Services.Repositories;

public interface ICityRepository
{
    IEnumerable<City> GetAll();
    City? GetByName(string name);
    void Update(City city);
    void Add(City city);
    void Delete(City city);
}