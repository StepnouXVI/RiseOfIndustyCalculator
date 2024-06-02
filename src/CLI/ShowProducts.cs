﻿using Domain;
using Spectre.Console;
using Services;
using Spectre.Console.Cli;

namespace CLI;

public class ShowProducts(IProductsRepository productsRepository): Command
{
    public override int Execute(CommandContext context)
    {
        var products = productsRepository.GetAllProducts();

        var table = new Table()
            .RoundedBorder()
            .AddColumn("Id")
            .AddColumn("Name");
        foreach (var product in products)
        {
            table.AddRow(product.Id.ToString(), product.Name);
        }
        
        AnsiConsole.Write(table);
        return 0;
    }

}