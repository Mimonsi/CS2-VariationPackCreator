namespace VariationPackCreator.Models
{
    public class Pack
    {
        public string Name { get; set; }
        public Dictionary<string, List<Variation>> Entries { get; set; }
    }
}