using Domain;

namespace Services;

public class CalculationService(
    IFactoriesRepository factoriesRepository,
    IProductRepository productRepository,
    IRecipesRepository recipesRepository)
{
    private readonly IFactoriesRepository _factoriesRepository = factoriesRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IRecipesRepository _recipesRepository = recipesRepository;

    public Task<ProductionChain> CreateProductionChain(ulong productId, double requiredQuantity)
    {
        var product = _productRepository.GetProduct(productId);
        var recipe = _recipesRepository.GetRecipeByProductId(productId);
        var factory = _factoriesRepository.GetFactory(recipe.FactoryId);

        var numberOfFactories = (requiredQuantity / recipe.OutputProducts[productId]);
        var root = new ProductionChainNode(factory, numberOfFactories, factory.Price*numberOfFactories, recipe);
        
        double totalPrice = FillChildren(root);
        
        var result = new ProductionChain(root);
        result.TotalPrice = totalPrice;
        
        return Task.FromResult(result);
    }
    
    private double FillChildren(ProductionChainNode parent)
    {
        double totalPrice = 0;
        var inputProducts = parent.Recipe.InputProducts;
        foreach (var input in inputProducts)
        {
            var requiredQuantity = parent.Quantity * input.Value;
            
            var recipe = _recipesRepository.GetRecipeByProductId(input.Key);
            var factory = _factoriesRepository.GetFactory(recipe.FactoryId);
            
            var numberOfFactories = (requiredQuantity / recipe.OutputProducts[input.Key]);
            var child = new ProductionChainNode(factory, numberOfFactories, numberOfFactories * factory.Price, recipe);
            
            totalPrice+= child.Quantity * child.Price;
            totalPrice += FillChildren(child);
            
            parent.Children.Add(child);
        }
        return totalPrice; 
    }
    
}