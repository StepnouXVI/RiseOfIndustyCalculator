using Domain;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI;

internal class AddProductCommand(IProductsRepository productsRepository) : Command<AddSettings>
{

    
    public override int Execute(CommandContext context, AddSettings settings)
    {
        for (int i = 0; i < settings.Number; i++)
        {
            productsRepository.AddProduct(InputProduct());
        }
        return 0;
    }
    
    private Product InputProduct()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the [bold][white]name[/][/] of the product?"));
        var id = AnsiConsole.Prompt(
            new TextPrompt<ulong>("What is the [bold][white]id[/][/] of the product?"));

        return new Product(name, id);
    }
}