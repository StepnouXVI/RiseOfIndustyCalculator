using Domain;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

public class Calculate(IRecipesRepository recipesRepository, IProductsRepository productsRepository, IFactoriesRepository factoriesRepository) : Command
{
    private CalculationService _calculation = new CalculationService(factoriesRepository, productsRepository, recipesRepository);
    public override int Execute(CommandContext context)
    {
        var products = productsRepository.GetAllProducts();
        
        var selectedProduct = AnsiConsole.Prompt(
            new SelectionPrompt<Product>()
                .Title("What product do you want to produce?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal Product)[/]")
                .AddChoices(products)
                .UseConverter(p => $"[yellow]{p.Id}[/] - [bold][white]{p.Name}[/][/]"));

        var quantity = AnsiConsole.Prompt(new TextPrompt<double>("How much of this product do you want to produce in 30 days?"));

        var productionChain = _calculation.CreateProductionChain(selectedProduct.Id, quantity);
        
        productionChain.PrintAsTree();

        return 0;
    }
}