using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using CLI;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;


using var factoriesRepos = new FactoryRepositoryJson("Q:\\RiseOfIndustyCalculator\\Factories.json");
using var productRepos = new ProductsRepositoryJson("Q:\\RiseOfIndustyCalculator\\Products.json");
using var recipesRepos = new RecipeRepositoryJson("Q:\\RiseOfIndustyCalculator\\Recipes.json");

var serviceCollections = new ServiceCollection();
serviceCollections.AddSingleton<IFactoriesRepository>(factoriesRepos);
serviceCollections.AddSingleton<IProductsRepository>(productRepos);
serviceCollections.AddSingleton<IRecipesRepository>(recipesRepos);

var app = new CommandApp(new TypeRegistrar(serviceCollections));
app.Configure(config =>
{
    config.AddBranch<AddSettings>("add", add =>
    {
        add.AddCommand<AddFactoryCommand>("factory");
        add.AddCommand<AddProductCommand>("product");
        add.AddCommand<AddRecipeCommand>("recipe");
    });
    
    config.AddBranch("show", show =>
    {
        show.AddCommand<ShowProducts>("products");
        show.AddCommand<ShowFactories>("factories");
        show.AddCommand<ShowRecipes>("recipes");
    });
    
    config.SetExceptionHandler((ex, resolver) =>
    {
        var panel = new Panel(ex.Message);

        panel.Header("[bold][white][[Error]][/][/]");
        panel.HeaderAlignment(Justify.Center);
        panel.Expand = true;
        panel.Border = BoxBorder.Double;
        panel.BorderColor(Color.Red);

        AnsiConsole.Write(panel);
    });
    config.PropagateExceptions();
});

return app.Run(args);
