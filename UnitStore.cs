using UnitConverterAPI.Models;

namespace UnitConverterAPI.Data
{
    // All units and their conversion factors are stored here.
    // Base units:
    //   Length    -> meter (m)
    //   Weight    -> kilogram (kg)
    //   Temperature -> celsius (special case, not factor-based)
    //   Volume    -> liter (L)
    //   Speed     -> meters per second (m/s)
    public static class UnitStore
    {
        // key is the unit name in lowercase for easy lookup
        private static readonly Dictionary<string, UnitDefinition> _units = new(StringComparer.OrdinalIgnoreCase)
        {
            // --- LENGTH ---
            { "meter",       new UnitDefinition { Name = "meter",       ToBaseFactor = 1,          Category = "length" } },
            { "m",           new UnitDefinition { Name = "meter",       ToBaseFactor = 1,          Category = "length" } },
            { "kilometer",   new UnitDefinition { Name = "kilometer",   ToBaseFactor = 1000,       Category = "length" } },
            { "km",          new UnitDefinition { Name = "kilometer",   ToBaseFactor = 1000,       Category = "length" } },
            { "centimeter",  new UnitDefinition { Name = "centimeter",  ToBaseFactor = 0.01,       Category = "length" } },
            { "cm",          new UnitDefinition { Name = "centimeter",  ToBaseFactor = 0.01,       Category = "length" } },
            { "millimeter",  new UnitDefinition { Name = "millimeter",  ToBaseFactor = 0.001,      Category = "length" } },
            { "mm",          new UnitDefinition { Name = "millimeter",  ToBaseFactor = 0.001,      Category = "length" } },
            { "mile",        new UnitDefinition { Name = "mile",        ToBaseFactor = 1609.344,   Category = "length" } },
            { "miles",       new UnitDefinition { Name = "mile",        ToBaseFactor = 1609.344,   Category = "length" } },
            { "yard",        new UnitDefinition { Name = "yard",        ToBaseFactor = 0.9144,     Category = "length" } },
            { "yards",       new UnitDefinition { Name = "yard",        ToBaseFactor = 0.9144,     Category = "length" } },
            { "yd",          new UnitDefinition { Name = "yard",        ToBaseFactor = 0.9144,     Category = "length" } },
            { "foot",        new UnitDefinition { Name = "foot",        ToBaseFactor = 0.3048,     Category = "length" } },
            { "feet",        new UnitDefinition { Name = "foot",        ToBaseFactor = 0.3048,     Category = "length" } },
            { "ft",          new UnitDefinition { Name = "foot",        ToBaseFactor = 0.3048,     Category = "length" } },
            { "inch",        new UnitDefinition { Name = "inch",        ToBaseFactor = 0.0254,     Category = "length" } },
            { "inches",      new UnitDefinition { Name = "inch",        ToBaseFactor = 0.0254,     Category = "length" } },
            { "in",          new UnitDefinition { Name = "inch",        ToBaseFactor = 0.0254,     Category = "length" } },
            { "nautical mile", new UnitDefinition { Name = "nautical mile", ToBaseFactor = 1852,   Category = "length" } },
            { "nm",          new UnitDefinition { Name = "nautical mile", ToBaseFactor = 1852,     Category = "length" } },

            // --- WEIGHT / MASS ---
            { "kilogram",    new UnitDefinition { Name = "kilogram",    ToBaseFactor = 1,          Category = "weight" } },
            { "kg",          new UnitDefinition { Name = "kilogram",    ToBaseFactor = 1,          Category = "weight" } },
            { "gram",        new UnitDefinition { Name = "gram",        ToBaseFactor = 0.001,      Category = "weight" } },
            { "g",           new UnitDefinition { Name = "gram",        ToBaseFactor = 0.001,      Category = "weight" } },
            { "milligram",   new UnitDefinition { Name = "milligram",   ToBaseFactor = 0.000001,   Category = "weight" } },
            { "mg",          new UnitDefinition { Name = "milligram",   ToBaseFactor = 0.000001,   Category = "weight" } },
            { "pound",       new UnitDefinition { Name = "pound",       ToBaseFactor = 0.453592,   Category = "weight" } },
            { "pounds",      new UnitDefinition { Name = "pound",       ToBaseFactor = 0.453592,   Category = "weight" } },
            { "lb",          new UnitDefinition { Name = "pound",       ToBaseFactor = 0.453592,   Category = "weight" } },
            { "lbs",         new UnitDefinition { Name = "pound",       ToBaseFactor = 0.453592,   Category = "weight" } },
            { "ounce",       new UnitDefinition { Name = "ounce",       ToBaseFactor = 0.0283495,  Category = "weight" } },
            { "ounces",      new UnitDefinition { Name = "ounce",       ToBaseFactor = 0.0283495,  Category = "weight" } },
            { "oz",          new UnitDefinition { Name = "ounce",       ToBaseFactor = 0.0283495,  Category = "weight" } },
            { "ton",         new UnitDefinition { Name = "ton",         ToBaseFactor = 1000,       Category = "weight" } },
            { "tonne",       new UnitDefinition { Name = "ton",         ToBaseFactor = 1000,       Category = "weight" } },
            { "metric ton",  new UnitDefinition { Name = "ton",         ToBaseFactor = 1000,       Category = "weight" } },
            { "stone",       new UnitDefinition { Name = "stone",       ToBaseFactor = 6.35029,    Category = "weight" } },
            { "st",          new UnitDefinition { Name = "stone",       ToBaseFactor = 6.35029,    Category = "weight" } },

            // --- TEMPERATURE (special - IsTemperature = true, factors are ignored for conversion) ---
            { "celsius",     new UnitDefinition { Name = "celsius",     ToBaseFactor = 1, Category = "temperature", IsTemperature = true } },
            { "c",           new UnitDefinition { Name = "celsius",     ToBaseFactor = 1, Category = "temperature", IsTemperature = true } },
            { "fahrenheit",  new UnitDefinition { Name = "fahrenheit",  ToBaseFactor = 1, Category = "temperature", IsTemperature = true } },
            { "f",           new UnitDefinition { Name = "fahrenheit",  ToBaseFactor = 1, Category = "temperature", IsTemperature = true } },
            { "kelvin",      new UnitDefinition { Name = "kelvin",      ToBaseFactor = 1, Category = "temperature", IsTemperature = true } },
            { "k",           new UnitDefinition { Name = "kelvin",      ToBaseFactor = 1, Category = "temperature", IsTemperature = true } },

            // --- VOLUME ---
            { "liter",       new UnitDefinition { Name = "liter",       ToBaseFactor = 1,          Category = "volume" } },
            { "litre",       new UnitDefinition { Name = "liter",       ToBaseFactor = 1,          Category = "volume" } },
            { "l",           new UnitDefinition { Name = "liter",       ToBaseFactor = 1,          Category = "volume" } },
            { "milliliter",  new UnitDefinition { Name = "milliliter",  ToBaseFactor = 0.001,      Category = "volume" } },
            { "millilitre",  new UnitDefinition { Name = "milliliter",  ToBaseFactor = 0.001,      Category = "volume" } },
            { "ml",          new UnitDefinition { Name = "milliliter",  ToBaseFactor = 0.001,      Category = "volume" } },
            { "gallon",      new UnitDefinition { Name = "gallon",      ToBaseFactor = 3.78541,    Category = "volume" } },  // US gallon
            { "gallons",     new UnitDefinition { Name = "gallon",      ToBaseFactor = 3.78541,    Category = "volume" } },
            { "gal",         new UnitDefinition { Name = "gallon",      ToBaseFactor = 3.78541,    Category = "volume" } },
            { "quart",       new UnitDefinition { Name = "quart",       ToBaseFactor = 0.946353,   Category = "volume" } },
            { "quarts",      new UnitDefinition { Name = "quart",       ToBaseFactor = 0.946353,   Category = "volume" } },
            { "qt",          new UnitDefinition { Name = "quart",       ToBaseFactor = 0.946353,   Category = "volume" } },
            { "pint",        new UnitDefinition { Name = "pint",        ToBaseFactor = 0.473176,   Category = "volume" } },
            { "pints",       new UnitDefinition { Name = "pint",        ToBaseFactor = 0.473176,   Category = "volume" } },
            { "pt",          new UnitDefinition { Name = "pint",        ToBaseFactor = 0.473176,   Category = "volume" } },
            { "cup",         new UnitDefinition { Name = "cup",         ToBaseFactor = 0.236588,   Category = "volume" } },
            { "cups",        new UnitDefinition { Name = "cup",         ToBaseFactor = 0.236588,   Category = "volume" } },
            { "fluid ounce", new UnitDefinition { Name = "fluid ounce", ToBaseFactor = 0.0295735,  Category = "volume" } },
            { "fl oz",       new UnitDefinition { Name = "fluid ounce", ToBaseFactor = 0.0295735,  Category = "volume" } },
            { "tablespoon",  new UnitDefinition { Name = "tablespoon",  ToBaseFactor = 0.0147868,  Category = "volume" } },
            { "tbsp",        new UnitDefinition { Name = "tablespoon",  ToBaseFactor = 0.0147868,  Category = "volume" } },
            { "teaspoon",    new UnitDefinition { Name = "teaspoon",    ToBaseFactor = 0.00492892, Category = "volume" } },
            { "tsp",         new UnitDefinition { Name = "teaspoon",    ToBaseFactor = 0.00492892, Category = "volume" } },

            // --- SPEED ---
            { "meters per second",       new UnitDefinition { Name = "meters per second",       ToBaseFactor = 1,         Category = "speed" } },
            { "m/s",                     new UnitDefinition { Name = "meters per second",       ToBaseFactor = 1,         Category = "speed" } },
            { "kilometers per hour",     new UnitDefinition { Name = "kilometers per hour",     ToBaseFactor = 0.277778,  Category = "speed" } },
            { "km/h",                    new UnitDefinition { Name = "kilometers per hour",     ToBaseFactor = 0.277778,  Category = "speed" } },
            { "kph",                     new UnitDefinition { Name = "kilometers per hour",     ToBaseFactor = 0.277778,  Category = "speed" } },
            { "miles per hour",          new UnitDefinition { Name = "miles per hour",          ToBaseFactor = 0.44704,   Category = "speed" } },
            { "mph",                     new UnitDefinition { Name = "miles per hour",          ToBaseFactor = 0.44704,   Category = "speed" } },
            { "knot",                    new UnitDefinition { Name = "knot",                    ToBaseFactor = 0.514444,  Category = "speed" } },
            { "knots",                   new UnitDefinition { Name = "knot",                    ToBaseFactor = 0.514444,  Category = "speed" } },
            { "feet per second",         new UnitDefinition { Name = "feet per second",         ToBaseFactor = 0.3048,    Category = "speed" } },
            { "ft/s",                    new UnitDefinition { Name = "feet per second",         ToBaseFactor = 0.3048,    Category = "speed" } },
        };

        public static UnitDefinition? GetUnit(string unitName)
        {
            if (string.IsNullOrWhiteSpace(unitName))
                return null;

            _units.TryGetValue(unitName.Trim(), out var unit);
            return unit;
        }

        public static IEnumerable<string> GetAllSupportedUnits()
        {
            // Return distinct unit names (not abbreviations)
            return _units.Values
                .Select(u => $"{u.Name} ({u.Category})")
                .Distinct()
                .OrderBy(x => x);
        }

        public static IEnumerable<string> GetUnitsByCategory(string category)
        {
            return _units.Values
                .Where(u => u.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .Select(u => u.Name)
                .Distinct()
                .OrderBy(x => x);
        }
    }
}
