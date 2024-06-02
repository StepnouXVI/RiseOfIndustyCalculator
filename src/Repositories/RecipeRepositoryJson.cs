using System.Text.Json;
using Domain;
using Services;

namespace Repositories;

public class RecipeRepositoryJson : IRecipesRepository, IDisposable
{
    private string _path;
    private Dictionary<ulong, Recipe> _recipes = new Dictionary<ulong, Recipe>();

    public RecipeRepositoryJson(string path)
    {
        _path = path;
        if (File.Exists(path))
        {
            LoadRecipes();
        }
    }

    private void LoadRecipes()
    {
        using FileStream stream = File.OpenRead(_path);
        var list = JsonSerializer.Deserialize<List<Recipe>>(stream);

        if (list is null)
        {
            throw new Exception("File is empty");
        }

        foreach (var recipe in list)
        {
            _recipes[recipe.Id] = recipe;
        }
    }

    public Recipe GetRecipe(ulong id)
    {
        if (!_recipes.TryGetValue(id, out var recipe))
            throw new Exception($"Recipe with id {id} not found");

        return recipe;
    }

    public void AddRecipe(Recipe recipe)
    {
        _recipes[recipe.Id] = recipe;
    }

    public void UpdateRecipe(Recipe recipe)
    {
        _recipes[recipe.Id] = recipe;
    }

    public void DeleteRecipe(ulong id)
    {
        _recipes.Remove(id);
    }

    public Recipe GetRecipeByProductId(ulong productId)
    {
        var result = _recipes.Values.FirstOrDefault(x => x.OutputProducts.ContainsKey(productId));
        if (result is null)
            throw new Exception($"Recipe with product id {productId} not found");

        return result;
    }

    public List<Recipe> GetRecipesByFactoryId(ulong factoryId)
    {
        var result = _recipes.Values
            .Where(x => x.FactoryId == factoryId)
            .ToList();
        
        return result;
    }

    public List<Recipe> GetAllRecipes()
    {
        return _recipes.Values.ToList();
    }

    public void Dispose()
    {
        using FileStream stream = File.Create(_path);
        var list = _recipes.Values.ToList();
        JsonSerializer.Serialize(stream, list);
    }
}