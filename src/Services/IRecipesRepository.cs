using Domain;

namespace Services;

public interface IRecipesRepository
{
    Recipe GetRecipe(ulong id);
    void AddRecipe(Recipe recipe);
    void UpdateRecipe(Recipe recipe);
    void DeleteRecipe(ulong id);
    Recipe GetRecipeByProductId(ulong productId);
    List<Recipe> GetRecipesByFactoryId(ulong factoryId);
    List<Recipe> GetAllRecipes();
}