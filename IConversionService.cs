using UnitConverterAPI.Models;

namespace UnitConverterAPI.Services
{
    public interface IConversionService
    {
        ConversionResponse Convert(ConversionRequest request);
        IEnumerable<string> GetSupportedUnits();
        IEnumerable<string> GetUnitsByCategory(string category);
    }
}
