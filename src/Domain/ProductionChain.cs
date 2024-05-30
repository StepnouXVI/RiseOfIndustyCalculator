namespace Domain;

public class ProductionChain
{
    public ProductionChainNode Root { get; init; }
    public double TotalPrice { get; set; } = 0;
    public ProductionChain(ProductionChainNode root)
    {
        Root = root;
    }
}