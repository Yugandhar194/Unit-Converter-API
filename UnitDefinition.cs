namespace UnitConverterAPI.Models
{
    // This represents a unit and its factor relative to the base unit for its category.
    // e.g. for length: base unit is meter, so "kilometer" has factor 1000 (1 km = 1000 m)
    public class UnitDefinition
    {
        public string Name { get; set; } = string.Empty;

        // Factor to convert THIS unit TO the base unit
        public double ToBaseFactor { get; set; }

        public string Category { get; set; } = string.Empty;

        // Some units (like temperature) can't just be multiplied - they need special handling
        public bool IsTemperature { get; set; } = false;
    }
}
