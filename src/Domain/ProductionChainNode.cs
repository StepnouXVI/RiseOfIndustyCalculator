namespace Domain;

public class ProductionChainNode(Factory factory, double quantity, double price, Recipe recipe, Product product)
{
    public Factory Factory { get; init; } = factory;
    public Recipe Recipe { get; set; } = recipe;
    public Product Product { get; init; } = product;
    public double Quantity { get; init; } = quantity;
    public double Price { get; init; } = price;



    public List<ProductionChainNode> Children { get; set; } = new List<ProductionChainNode>();
}