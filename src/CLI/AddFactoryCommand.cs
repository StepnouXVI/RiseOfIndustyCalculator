using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Domain;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI;

internal class AddCommand : Command<AddCommand.AddCommandSettings>
{
    private IFactoriesRepository _factoriesRepository;
    private IRecipesRepository _recipesRepository;
    private IProductsRepository _productsRepository;
    
    internal class AddCommandSettings : CommandSettings
    {
        [CommandOption("-t|--type")] public string? Type { get; set; }

        [CommandOption("-n|--number")]
        [DefaultValue(1)]
        public uint? Number { get; set; }
    }


    public override int Execute(CommandContext context, AddCommandSettings settings)
    {
        if (settings.Type is null)
        {
            settings.Type = AskType();
        }

        return 0;
    }

    private string AskType()
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What do you want to add?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal Type)[/]")
                .AddChoices(new[]
                {
                    "Factory",
                    "Recipe",
                    "Product",
                }));
    }

    private Factory InputFactory()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the name of the factory?"));
        var price = AnsiConsole.Prompt(
            new TextPrompt<double>("What is the price of the factory?").Validate(p =>
            {
                if (p <= 0)
                {
                    return ValidationResult.Error("Price must be greater than 0");
                }

                return ValidationResult.Success();
            }));

        var id = AnsiConsole.Prompt(new TextPrompt<uint>("What is the id of the factory?"));

        return new Factory(
            price,
            name,
            id
        );
    }

    private ulong AskRecipeId()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<ulong>("What is the ID of the recipe?"));
    }

    private Factory AskFactory(IEnumerable<Factory> factories)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<Factory>()
                .Title("Which factory is this recipe for?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal Factory)[/]")
                .AddChoices(factories)
                .UseConverter(f => $"{f.Id} - {f.Name}"));
    }

    private Dictionary<ulong, ulong> AskProducts(string title, IEnumerable<Product> products)
    {
        var selectedProducts = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Product>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal Product)[/]")
                .AddChoices(products)
                .UseConverter(p => $"{p.Id} - {p.Name}"));

        var productQuantities = new Dictionary<ulong, ulong>();

        foreach (var product in selectedProducts)
        {
            var quantity = AnsiConsole.Prompt(
                new TextPrompt<ulong>($"How much of the product ({product.Name}) do you need?"));
            productQuantities[product.Id] = quantity;
        }

        return productQuantities;
    }

    private uint AskProductionTime()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<uint>("How long does it take to produce?"));
    }

    private Recipe InputRecipe()
    {
        var factories = _factoriesRepository.GetAllFactories();
        var products = _productsRepository.GetAllProducts();

        var id = AskRecipeId();
        var factory = AskFactory(factories);
        var inputProducts = AskProducts("Choose the input products", products);
        var outputProducts = AskProducts("Choose the output products", products);
        var productionTime = AskProductionTime();

        return new Recipe(inputProducts, outputProducts, id, factory.Id, productionTime);
    }

    private Product InputProduct()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the name of the product?"));
        var id = AnsiConsole.Prompt(
            new TextPrompt<ulong>("What is the id of the product?"));

        return new Product(name, id);

    }
}