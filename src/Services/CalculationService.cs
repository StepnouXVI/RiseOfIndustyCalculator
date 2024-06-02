using Domain;

namespace Services;

public class CalculationService(
    IFactoriesRepository factoriesRepository,
    IProductsRepository productsRepository,
    IRecipesRepository recipesRepository)
{
    public ProductionChain CreateProductionChain(ulong productId, double requiredQuantity)
    {
        var product = productsRepository.GetProduct(productId);
        var recipe = recipesRepository.GetRecipeByProductId(productId);
        var factory = factoriesRepository.GetFactory(recipe.FactoryId);

        var numberOfFactories = (requiredQuantity / recipe.OutputProducts[productId])*recipe.ProductionTime/30.0;
        var root = new ProductionChainNode(factory, numberOfFactories, factory.Price*numberOfFactories, recipe, product);
        
        double totalPrice = FillChildren(root);
        
        var result = new ProductionChain(root);
        result.TotalPrice = totalPrice;
        
        return result;
    }
    
    private double FillChildren(ProductionChainNode parent)
    {
        double totalPrice = 0;
        var inputProducts = parent.Recipe.InputProducts;
        
        foreach (var input in inputProducts)
        {
            var product = productsRepository.GetProduct(input.Key);
            var recipe = recipesRepository.GetRecipeByProductId(input.Key);
            var factory = factoriesRepository.GetFactory(recipe.FactoryId);
            
            var requiredQuantity = parent.Quantity * input.Value*30.0/recipe.ProductionTime;
            var numberOfFactories = (requiredQuantity / recipe.OutputProducts[input.Key])*recipe.ProductionTime/30.0;
            var child = new ProductionChainNode(factory, numberOfFactories, numberOfFactories * factory.Price, recipe, product);
            
            totalPrice += child.Price;
            totalPrice += FillChildren(child);
            
            parent.Children.Add(child);
        }
        return totalPrice; 
    } 
    
}