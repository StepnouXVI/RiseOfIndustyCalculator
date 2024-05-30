using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Factory
    {
        public double Price { get; init; } = 0;

        public double MonthlyAmortization { get; init; } = 0;

        public string Name { get; init; }
        
        public ulong Id { get; init; }
    }
}