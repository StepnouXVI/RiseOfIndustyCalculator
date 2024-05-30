namespace Domain;

public class ProductionChainNode
{
    public Factory Factory { get; init; }
    public double Quantity { get; init; }
    public double Price { get; init; }
    
    public Recipe Recipe { get; set; }
    
    public List<ProductionChainNode> Children { get; set; } = new List<ProductionChainNode>();
    
    public ProductionChainNode(Factory factory, double quantity, double price, Recipe recipe)
    {
        Factory = factory;
        Quantity = quantity;
        Price = price;
        Recipe = recipe;
    }
}