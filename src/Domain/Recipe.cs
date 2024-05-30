namespace Domain
{
    public class Recipe
    {
        public ulong Id { get; init; }
        public string Name { get; init; }
        
        public uint FactoryId { get; init; }
        public Dictionary<ulong, ulong> InputProducts { get; init; } = new Dictionary<ulong, ulong>();
        public Dictionary<ulong, ulong> OutputProducts { get; init; } = new Dictionary<ulong, ulong>();

        public Recipe(string name, Dictionary<ulong, ulong> inputProducts, Dictionary<ulong, ulong> outputProducts, ulong id, uint factoryId)
        {
            Name = name;
            InputProducts = inputProducts;
            OutputProducts = outputProducts;
            Id = id;
            FactoryId = factoryId;
        }

        public Recipe(string name, Dictionary<ulong, ulong> outputProducts, ulong id, uint factoryId)
        {
            Name = name;
            OutputProducts = outputProducts;
            Id = id;
            FactoryId = factoryId;
        }
    }
}