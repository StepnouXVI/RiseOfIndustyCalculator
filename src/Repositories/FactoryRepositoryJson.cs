using System.Text.Json;
using Domain;
using Services;

namespace Repositories;

public class FactoryRepositoryJson  : IFactoriesRepository, IDisposable
{
    private readonly string _path;
    private readonly Dictionary<ulong, Factory> _factories = new Dictionary<ulong, Factory>();

    public FactoryRepositoryJson(string path)
    {
        _path = path;
        if (File.Exists(path))
        {
            LoadFactories();
        }
    }

    private void LoadFactories()
    {
        using FileStream stream = File.OpenRead(_path);
        var list = JsonSerializer.Deserialize<List<Factory>>(stream);

        if (list is null)
        {
            throw new Exception("File is empty");
        }

        foreach (var factory in list)
        {
            _factories[factory.Id] = factory;
        }
    }

    public Factory GetFactory(ulong id)
    {
        if (!_factories.TryGetValue(id, out var factory))
            throw new Exception($"Factory with id {id} not found");
        
        return factory;
    }

    public Factory GetFactoryByName(string name)
    {
        var result = _factories.Values.FirstOrDefault(x => x.Name == name);
        if(result is null)
            throw new Exception($"Factory with name {name} not found");
        
        return result;
    }

    public void AddFactory(Factory factory)
    {
        _factories[factory.Id] =  factory;
    }

    public void UpdateFactory(Factory factory)
    {
        _factories[factory.Id] =  factory;
    }

    public void DeleteFactory(ulong id)
    {
        _factories.Remove(id);
    }

    public List<Factory> GetAllFactories()
    {
        return _factories.Values.ToList();
    }

    public void Dispose()
    {
        using FileStream stream = File.Create(_path);
        var list = _factories.Values.ToList();
        JsonSerializer.Serialize(stream, list);
    }
}