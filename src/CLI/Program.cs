using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using CLI;
using Domain;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp<FileSizeCommand>();
app.Configure(config =>
{
    config.SetExceptionHandler((ex, resolver) =>
    {
        var panel = new Panel(ex.Message);

        panel.Header("[white][[Error]][/]");
        panel.HeaderAlignment(Justify.Center);
        panel.Expand = true;
        panel.Border = BoxBorder.Double;
        panel.BorderColor(Color.Red);

        AnsiConsole.Write(panel);
    });
});

return app.Run(args);

internal sealed class FileSizeCommand : Command
{
    public override int Execute(CommandContext context)
    {
        var factory = new Factory(56.2, "MyFactory", 1);
        factory.Print();
        


        return 0;
    }
}