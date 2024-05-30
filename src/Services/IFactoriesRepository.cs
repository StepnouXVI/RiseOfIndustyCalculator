namespace Services;
using Domain;
public interface IFactoriesRepository
{
    Factory GetFactory(ulong id);
    void AddFactory(Factory factory);
    void UpdateFactory(Factory factory);
    void DeleteFactory(ulong id);
}