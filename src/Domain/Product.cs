using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Product
    {
        public ulong Id { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }

        public Product(string name, double price, ulong id)
        {
            Name = name;
            Price = price;
            Id = id;
        }
    }
}