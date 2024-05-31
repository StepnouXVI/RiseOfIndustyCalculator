namespace Services;
using Domain;
public interface IProductsRepository
{
    Product GetProduct(ulong id);
    Product GetProductByName(string name);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(ulong id);
    List<Product> GetAllProducts();
}