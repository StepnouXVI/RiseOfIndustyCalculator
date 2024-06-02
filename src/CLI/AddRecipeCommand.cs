using Domain;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI;

internal class AddRecipeCommand(
    IFactoriesRepository factoriesRepository,
    IRecipesRepository recipesRepository,
    IProductsRepository productsRepository) : Command<AddSettings>
{
    public override int Execute(CommandContext context, AddSettings settings)
    {
        for (int i = 0; i < settings.Number; i++)
        {
            recipesRepository.AddRecipe(InputRecipe());
        }

        return 0;
    }
    
    private Recipe InputRecipe()
    {
        var factories = factoriesRepository.GetAllFactories();
        var products = productsRepository.GetAllProducts();

        var id = AskRecipeId();
        var factory = AskFactory(factories);
        
        var inputProducts = new Dictionary<ulong, ulong>();
        if(AnsiConsole.Confirm("Does this recipe have any [bold][white]input[/][/] products?"))
            inputProducts = AskProducts("Choose the [bold][white]input products[/][/]", products);
        
        var outputProducts = AskProducts("Choose the [bold][white]output products[/][/]", products);
        var productionTime = AskProductionTime();

        return new Recipe(inputProducts, outputProducts, id, factory.Id, productionTime);
    }
    
    private ulong AskRecipeId()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<ulong>("What is the [bold][white]Id[/][/] of the recipe?"));
    }
    
    private Factory AskFactory(IEnumerable<Factory> factories)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<Factory>()
                .Title("Which [bold][white]factory[/][/] is this recipe for?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal [/][bold][white]Factory)[/][/]")
                .AddChoices(factories)
                .UseConverter(f => $"[yellow]{f.Id}[/] - [bold][white]{f.Name}[/][/]"));
    }
    
    private Dictionary<ulong, ulong> AskProducts(string title, IEnumerable<Product> products)
    {
        var selectedProducts = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Product>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal Product)[/]")
                .AddChoices(products)
                .UseConverter(p => $"[yellow]{p.Id}[/] - [bold][white]{p.Name}[/][/]"));

        var productQuantities = new Dictionary<ulong, ulong>();

        foreach (var product in selectedProducts)
        {
            var quantity = AnsiConsole.Prompt(
                new TextPrompt<ulong>($"How much of the product [yellow]({product.Name})[/] do you need?"));
            productQuantities[product.Id] = quantity;
        }

        return productQuantities;
    }
    
    private uint AskProductionTime()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<uint>("How long does it take to produce?"));
    }
}