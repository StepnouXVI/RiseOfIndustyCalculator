using Domain;
using Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI;

internal class AddFactoryCommand(IFactoriesRepository factoriesRepository) : Command<AddSettings>
{
    public override int Execute(CommandContext context, AddSettings settings)
    {
        for (int i = 0; i < settings.Number; i++)
        {
            factoriesRepository.AddFactory(InputFactory());
        }
        return 0;
    }
    
    private Factory InputFactory()
    {
        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the [bold][white]name[/][/] of the factory?"));
        var price = AnsiConsole.Prompt(
            new TextPrompt<double>("What is the [bold][white]price[/][/] of the factory?").Validate(p =>
            {
                if (p <= 0)
                {
                    return ValidationResult.Error("[bold][white]Price[/] [red]must be greater than 0[/][/]");
                }

                return ValidationResult.Success();
            }));

        var id = AnsiConsole.Prompt(new TextPrompt<uint>("What is the [bold][white]id[/][/] of the factory?"));

        return new Factory(
            price,
            name,
            id
        );
    }
}