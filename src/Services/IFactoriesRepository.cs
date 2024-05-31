namespace Services;
using Domain;
public interface IFactoriesRepository
{
    Factory GetFactory(ulong id);
    Factory GetFactoryByName(string name);
    void AddFactory(Factory factory);
    void UpdateFactory(Factory factory);
    void DeleteFactory(ulong id);
    List<Factory> GetAllFactories();
    
}