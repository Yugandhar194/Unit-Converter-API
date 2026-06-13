namespace UnitConverterAPI.Models
{
    public class ConversionResponse
    {
        public double InputValue { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        public double ConvertedValue { get; set; }
        public string ToUnit { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
