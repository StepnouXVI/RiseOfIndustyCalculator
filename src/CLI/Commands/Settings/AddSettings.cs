using System.ComponentModel;
using Spectre.Console.Cli;

namespace CLI.Commands.Settings;

internal class AddSettings : CommandSettings
{
    [CommandOption("-n|--number")]
    [DefaultValue(1)]
    public int Number { get; set; }
}