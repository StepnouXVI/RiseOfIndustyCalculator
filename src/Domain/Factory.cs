using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Factory(double price, string name, ulong id)
    {
        public double Price { get; init; } = price;

        public string Name { get; init; } = name;

        public ulong Id { get; init; } = id;
    }
}