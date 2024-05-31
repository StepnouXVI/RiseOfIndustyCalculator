using Domain;

namespace Services;

public class CalculationService(
    IFactoriesRepository factoriesRepository,
    IProductsRepository productsRepository,
    IRecipesRepository recipesRepository)
{
    private readonly IFactoriesRepository _factoriesRepository = factoriesRepository;
    private readonly IProductsRepository _productsRepository = productsRepository;
    private readonly IRecipesRepository _recipesRepository = recipesRepository;

    public Task<ProductionChain> CreateProductionChain(ulong productId, double requiredQuantity)
    {
        var product = _productsRepository.GetProduct(productId);
        var recipe = _recipesRepository.GetRecipeByProductId(productId);
        var factory = _factoriesRepository.GetFactory(recipe.FactoryId);

        var numberOfFactories = (requiredQuantity / recipe.OutputProducts[productId])*recipe.ProductionTime/30.0;
        var root = new ProductionChainNode(factory, numberOfFactories, factory.Price*numberOfFactories, recipe, product);
        
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
            var product = _productsRepository.GetProduct(input.Key);
            var recipe = _recipesRepository.GetRecipeByProductId(input.Key);
            var factory = _factoriesRepository.GetFactory(recipe.FactoryId);
            
            var requiredQuantity = parent.Quantity * input.Value;
            var numberOfFactories = (requiredQuantity / recipe.OutputProducts[input.Key])*recipe.ProductionTime/30.0;
            var child = new ProductionChainNode(factory, numberOfFactories, numberOfFactories * factory.Price, recipe, product);
            
            totalPrice += child.Price;
            totalPrice += FillChildren(child);
            
            parent.Children.Add(child);
        }
        return totalPrice; 
    } 
    
}