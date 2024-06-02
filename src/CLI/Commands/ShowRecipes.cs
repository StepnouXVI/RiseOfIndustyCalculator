using Domain;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;
using CLI.Commands.Settings;

namespace CLI.Commands;

public class ShowRecipes(IRecipesRepository recipesRepository, IFactoriesRepository factoriesRepository)
    : Command<ShowRecipesSettings>
{
    private Table CreateTableOfProducts(Dictionary<ulong, ulong> products)
    {
        var table = new Table()
            .AddColumn("Product Id")
            .AddColumn("Amount");
        foreach (var product in products)
        {
            table.AddRow(product.Key.ToString(), product.Value.ToString());
        }

        return table;
    }

    public override int Execute(CommandContext context, ShowRecipesSettings settings)
    {
        List<Recipe> recipes;
        if (settings.FactoryName is null)
        {
            recipes = recipesRepository.GetAllRecipes();
        }
        else
        {
            var factory = factoriesRepository.GetFactoryByName(settings.FactoryName);
            recipes = recipesRepository.GetRecipesByFactoryId(factory.Id);
        }

        var table = new Table()
            .RoundedBorder()
            .AddColumn("Id")
            .AddColumn("Factory Id")
            .AddColumn("Production term")
            .AddColumn("Input")
            .AddColumn("Output")
            .ShowRowSeparators();

        foreach (var recipe in recipes)
        {
            table.AddRow(new Markup(recipe.Id.ToString()), new Markup(recipe.FactoryId.ToString()),new Markup(recipe.ProductionTime.ToString()),
                CreateTableOfProducts(recipe.InputProducts), CreateTableOfProducts(recipe.OutputProducts));
        }
        
        AnsiConsole.Write(table);
        return 0;
    }
}