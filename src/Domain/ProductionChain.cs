namespace Domain;

public class ProductionChain(ProductionChainNode root)
{
    public ProductionChainNode Root { get; init; } = root;
    public double TotalPrice { get; set; } = 0;
}