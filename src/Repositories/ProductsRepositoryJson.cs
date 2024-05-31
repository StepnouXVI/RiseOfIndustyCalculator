using System.Text.Json;
using Domain;
using Services;

namespace Repositories;

public class ProductsRepositoryJson : IProductsRepository, IDisposable
{
    private string _path;
    private readonly Dictionary<ulong, Product> _products = new Dictionary<ulong, Product>();

    public ProductsRepositoryJson(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("File not found");

        _path = path;
        LoadProducts();
    }
    private void LoadProducts()
    {
        using FileStream stream = File.OpenRead(_path);
        var list = JsonSerializer.Deserialize<List<Product>>(stream);

        if (list is null)
        {
            throw new Exception("File is empty");
        }

        foreach (var product in list)
        {
            _products[product.Id] = product;
        }
    }

    public void Dispose()
    {
        using FileStream stream = File.Create(_path);
        var list = _products.Values.ToList();
        JsonSerializer.Serialize(stream, list);
    }
    

    public Product GetProductByName(string name)
    {
        var result = _products.Values.FirstOrDefault(x => x.Name == name);
        if(result is null)
            throw new Exception($"Product with name {name} not found");
        
        return result;
    }

    public Product GetProduct(ulong id)
    {
        if (!_products.TryGetValue(id, out var product))
            throw new Exception($"Product with id {id} not found");
        
        return product;
    }

    public void AddProduct(Product product)
    {
        _products[product.Id] =  product;
    }

    public void UpdateProduct(Product product)
    {
        _products[product.Id] = product;
    }

    public void DeleteProduct(ulong id)
    {
        _products.Remove(id);
    }

    public List<Product> GetAllProducts()
    {
        return _products.Values.ToList();
    }
}