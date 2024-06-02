namespace Domain
{
    public class Recipe
    {
        public ulong Id { get; init; }
        public ulong FactoryId { get; init; }
        public uint ProductionTime { get; init; }
        public Dictionary<ulong, ulong> InputProducts { get; init; }
        public Dictionary<ulong, ulong> OutputProducts { get; init; }

        public Recipe(Dictionary<ulong, ulong> inputProducts, Dictionary<ulong, ulong> outputProducts, ulong id, ulong factoryId, uint productionTime)
        {
            InputProducts = inputProducts;
            OutputProducts = outputProducts;
            Id = id;
            FactoryId = factoryId;
            ProductionTime = productionTime;
        }
    }
}