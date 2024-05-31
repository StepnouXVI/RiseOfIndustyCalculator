using Domain;
using Spectre.Console;

namespace CLI;

public static class PrintHelper
{
    public static void Print(this Factory factory)
    {
        AnsiConsole.Write(factory.ConvertToTable());
    }

    public static Table ConvertToTable(this Factory factory)
    {
        return new Table()
            .RoundedBorder()
            .AddColumn("Id")
            .AddColumn("Name")
            .AddColumn("Price").AddRow(factory.Id.ToString(), factory.Name, factory.Price.ToString("0.##"));
    }

    public static void Print(this Recipe recipe)
    {
        AnsiConsole.Write(recipe.ConvertToTable());
    }

    public static Table ConvertToTable(this Recipe recipe)
    {
        var inputTable = new Table()
            .AddColumn("Product Id")
            .AddColumn("Amount");
        foreach (var inputProduct in recipe.InputProducts)
        {
            inputTable.AddRow(inputProduct.Key.ToString(), inputProduct.Value.ToString());
        }

        var outputTable = new Table()
            .AddColumn("Product Id")
            .AddColumn("Amount");
        foreach (var outputProduct in recipe.OutputProducts)
        {
            outputTable.AddRow(outputProduct.Key.ToString(), outputProduct.Value.ToString());
        }

        return new Table()
            .RoundedBorder()
            .AddColumn("Id")
            .AddColumn("Factory Id")
            .AddColumn("Input")
            .AddColumn("Output")
            .AddRow(new Markup(recipe.Id.ToString()), new Markup(recipe.FactoryId.ToString()), inputTable,
                outputTable);
    }
    
    public static void Print(this Product product)
    {
        AnsiConsole.Write(product.ConvertToTable());
    }

    public static Table ConvertToTable(this Product product)
    {
        return new Table()
            .RoundedBorder()
            .AddColumn("Id")
            .AddColumn("Name")
            .AddRow(product.Id.ToString(), product.Name);
    }

    public static void PrintAsTree(this ProductionChain productionChain)
    {
        AnsiConsole.Write(productionChain.ConvertToTree());
    }
    
    public static Tree ConvertToTree(this ProductionChain  productionChain)
    {
        var tree = new Tree(productionChain.Root.ConvertToTable());

        foreach (var child in productionChain.Root.Children)
        {
            tree.AddNode(child);
        }
        
        return tree;
    }

    private static Table ConvertToTable(this ProductionChainNode productionChainNode)
    {
        return new Table()
            .AddColumn("Factory name")
            .AddColumn("Number of factories")
            .AddColumn("Total price")
            .AddColumn("Product")
            .AddRow(productionChainNode.Factory.Name, productionChainNode.Quantity.ToString("0.##"), productionChainNode.Price.ToString("0.##"), productionChainNode.Product.Name);
    }
    
    
    private static void AddNode(this Tree tree, ProductionChainNode productionChainNode)
    {
        var node = tree.AddNode(productionChainNode.ConvertToTable());

        foreach (var child in productionChainNode.Children)
        {
            node.AddNode(child);
        }
    }
    
    private static void AddNode(this TreeNode root, ProductionChainNode productionChainNode)
    {
        var node = root.AddNode(productionChainNode.ConvertToTable());

        foreach (var child in productionChainNode.Children)
        {
            node.AddNode(child);
        }
    }
    
    
}
