namespace Services;
using Domain;
public interface IProductRepository
{
    Product GetProduct(ulong id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(ulong id);
}