using Spectre.Console.Cli;

namespace CLI;

public class ShowRecipesSettings : CommandSettings
{
    [CommandOption("-f|--factory")]
    public string? FactoryName { get; set; }
}