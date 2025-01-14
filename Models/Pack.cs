namespace VariationPackCreator.Models
{
    public class Pack
    {
        public required string Name { get; set; }
        public required Dictionary<string, List<Variation>> Entries { get; init; }
    }
}