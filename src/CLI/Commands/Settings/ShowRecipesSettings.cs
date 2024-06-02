using Spectre.Console.Cli;

namespace CLI.Commands.Settings;

public class ShowRecipesSettings : CommandSettings
{
    [CommandOption("-f|--factory")]
    public string? FactoryName { get; set; }
}