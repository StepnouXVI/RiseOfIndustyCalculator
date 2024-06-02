using Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

public class ShowFactories(IFactoriesRepository factoriesRepository) : Command
{
    public override int Execute(CommandContext context)
    {
        var factories = factoriesRepository.GetAllFactories();

        var table = new Table()
            .RoundedBorder()
            .AddColumn("Id")
            .AddColumn("Name")
            .AddColumn("Price");
        
        foreach (var factory in factories)
        {
            table.AddRow(factory.Id.ToString(), factory.Name, factory.Price.ToString("0.##"));
        }
        
        AnsiConsole.Write(table);
        return 0;
    }
}