using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Product(string name, ulong id)
    {
        public ulong Id { get; init; } = id;
        public string Name { get; init; } = name;
    }
}