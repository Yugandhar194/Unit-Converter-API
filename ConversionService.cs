using UnitConverterAPI.Data;
using UnitConverterAPI.Models;

namespace UnitConverterAPI.Services
{
    public class ConversionService : IConversionService
    {
        public ConversionResponse Convert(ConversionRequest request)
        {
            // basic null/empty checks
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.FromUnit))
                throw new ArgumentException("FromUnit cannot be empty.");

            if (string.IsNullOrWhiteSpace(request.ToUnit))
                throw new ArgumentException("ToUnit cannot be empty.");

            // check for NaN / Infinity - these should not be allowed
            if (double.IsNaN(request.Value) || double.IsInfinity(request.Value))
                throw new ArgumentException("Value must be a valid finite number.");

            var fromUnit = UnitStore.GetUnit(request.FromUnit);
            if (fromUnit == null)
                throw new ArgumentException($"Unknown unit: '{request.FromUnit}'. Check /api/conversion/units for supported units.");

            var toUnit = UnitStore.GetUnit(request.ToUnit);
            if (toUnit == null)
                throw new ArgumentException($"Unknown unit: '{request.ToUnit}'. Check /api/conversion/units for supported units.");

            // units must be in the same category
            if (!fromUnit.Category.Equals(toUnit.Category, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Cannot convert between different categories: '{fromUnit.Category}' and '{toUnit.Category}'.");

            double result;

            // temperature needs special handling because you can't just multiply
            if (fromUnit.IsTemperature)
            {
                result = ConvertTemperature(request.Value, fromUnit.Name, toUnit.Name);
            }
            else
            {
                // for everything else: convert to base unit first, then to target unit
                // e.g. 5 km -> meters: 5 * 1000 = 5000m -> feet: 5000 / 0.3048 = 16404ft
                double inBaseUnit = request.Value * fromUnit.ToBaseFactor;
                result = inBaseUnit / toUnit.ToBaseFactor;
            }

            // round to 10 decimal places to avoid floating point junk like 1.0000000000001
            result = Math.Round(result, 10);

            return new ConversionResponse
            {
                InputValue = request.Value,
                FromUnit = fromUnit.Name,
                ConvertedValue = result,
                ToUnit = toUnit.Name,
                Category = fromUnit.Category
            };
        }

        private double ConvertTemperature(double value, string from, string to)
        {
            // first convert everything to celsius, then to target
            // this is basically brute-force but it works for 3 units

            double celsius;

            switch (from.ToLower())
            {
                case "celsius":
                    celsius = value;
                    break;
                case "fahrenheit":
                    celsius = (value - 32) * 5.0 / 9.0;
                    break;
                case "kelvin":
                    // absolute zero check
                    if (value < 0)
                        throw new ArgumentException("Kelvin cannot be negative (below absolute zero).");
                    celsius = value - 273.15;
                    break;
                default:
                    throw new ArgumentException($"Unknown temperature unit: {from}");
            }

            // celsius -> target
            switch (to.ToLower())
            {
                case "celsius":
                    return celsius;
                case "fahrenheit":
                    return celsius * 9.0 / 5.0 + 32;
                case "kelvin":
                    double kelvin = celsius + 273.15;
                    // sanity check - result shouldn't be negative kelvin
                    if (kelvin < 0)
                        throw new ArgumentException("Conversion resulted in a temperature below absolute zero, which is not physically possible.");
                    return kelvin;
                default:
                    throw new ArgumentException($"Unknown temperature unit: {to}");
            }
        }

        public IEnumerable<string> GetSupportedUnits()
        {
            return UnitStore.GetAllSupportedUnits();
        }

        public IEnumerable<string> GetUnitsByCategory(string category)
        {
            var units = UnitStore.GetUnitsByCategory(category);

            // return empty list (not error) if category doesn't exist - let the controller decide what to do
            return units;
        }
    }
}
