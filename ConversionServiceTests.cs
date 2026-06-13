using UnitConverterAPI.Models;
using UnitConverterAPI.Services;
using Xunit;

namespace UnitConverterAPI.Tests
{
    public class ConversionServiceTests
    {
        private readonly ConversionService _service;

        public ConversionServiceTests()
        {
            _service = new ConversionService();
        }

        // ===================== HAPPY PATH TESTS =====================

        [Fact]
        public void Convert_KilometersToMiles_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "kilometer", ToUnit = "mile" };
            var result = _service.Convert(request);

            Assert.Equal("kilometer", result.FromUnit);
            Assert.Equal("mile", result.ToUnit);
            Assert.Equal(0.6213711922, result.ConvertedValue, precision: 5);
        }

        [Fact]
        public void Convert_MetersToFeet_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "meter", ToUnit = "foot" };
            var result = _service.Convert(request);

            // 1 meter = 3.28084 feet
            Assert.Equal(3.28084, result.ConvertedValue, precision: 4);
        }

        [Fact]
        public void Convert_SameUnit_ReturnsSameValue()
        {
            var request = new ConversionRequest { Value = 42, FromUnit = "kilogram", ToUnit = "kg" };
            var result = _service.Convert(request);

            Assert.Equal(42, result.ConvertedValue);
        }

        [Fact]
        public void Convert_PoundsToKilograms_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 10, FromUnit = "lbs", ToUnit = "kg" };
            var result = _service.Convert(request);

            // 10 lbs = 4.53592 kg
            Assert.Equal(4.53592, result.ConvertedValue, precision: 4);
        }

        // ===================== TEMPERATURE TESTS =====================

        [Fact]
        public void Convert_CelsiusToFahrenheit_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 100, FromUnit = "celsius", ToUnit = "fahrenheit" };
            var result = _service.Convert(request);

            Assert.Equal(212, result.ConvertedValue, precision: 4);
        }

        [Fact]
        public void Convert_FahrenheitToCelsius_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 32, FromUnit = "fahrenheit", ToUnit = "celsius" };
            var result = _service.Convert(request);

            Assert.Equal(0, result.ConvertedValue, precision: 4);
        }

        [Fact]
        public void Convert_CelsiusToKelvin_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 0, FromUnit = "celsius", ToUnit = "kelvin" };
            var result = _service.Convert(request);

            Assert.Equal(273.15, result.ConvertedValue, precision: 4);
        }

        [Fact]
        public void Convert_KelvinToCelsius_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 0, FromUnit = "kelvin", ToUnit = "celsius" };
            var result = _service.Convert(request);

            // 0 kelvin = -273.15 celsius (absolute zero)
            Assert.Equal(-273.15, result.ConvertedValue, precision: 4);
        }

        [Fact]
        public void Convert_AbsoluteZeroFahrenheit_ReturnsCorrectKelvin()
        {
            // -459.67 F is absolute zero
            var request = new ConversionRequest { Value = -459.67, FromUnit = "fahrenheit", ToUnit = "kelvin" };
            var result = _service.Convert(request);

            Assert.Equal(0, result.ConvertedValue, precision: 2);
        }

        // ===================== ABBREVIATION TESTS =====================

        [Fact]
        public void Convert_UsingAbbreviations_WorksCorrectly()
        {
            var request = new ConversionRequest { Value = 1000, FromUnit = "m", ToUnit = "km" };
            var result = _service.Convert(request);

            Assert.Equal(1, result.ConvertedValue);
        }

        [Fact]
        public void Convert_MixingAbbreviationAndFullName_WorksCorrectly()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "km", ToUnit = "meter" };
            var result = _service.Convert(request);

            Assert.Equal(1000, result.ConvertedValue);
        }

        // ===================== EDGE CASES =====================

        [Fact]
        public void Convert_ZeroValue_ReturnsZero()
        {
            var request = new ConversionRequest { Value = 0, FromUnit = "kilometer", ToUnit = "mile" };
            var result = _service.Convert(request);

            Assert.Equal(0, result.ConvertedValue);
        }

        [Fact]
        public void Convert_NegativeValue_WorksForNonTemperature()
        {
            // negative values are valid for things like coordinates or depth
            var request = new ConversionRequest { Value = -5, FromUnit = "meter", ToUnit = "foot" };
            var result = _service.Convert(request);

            Assert.True(result.ConvertedValue < 0);
        }

        [Fact]
        public void Convert_VeryLargeValue_WorksCorrectly()
        {
            var request = new ConversionRequest { Value = 1_000_000, FromUnit = "meter", ToUnit = "kilometer" };
            var result = _service.Convert(request);

            Assert.Equal(1000, result.ConvertedValue);
        }

        [Fact]
        public void Convert_VerySmallValue_WorksCorrectly()
        {
            var request = new ConversionRequest { Value = 0.001, FromUnit = "kilometer", ToUnit = "meter" };
            var result = _service.Convert(request);

            Assert.Equal(1, result.ConvertedValue, precision: 5);
        }

        // ===================== ERROR CASES =====================

        [Fact]
        public void Convert_NullRequest_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Convert(null!));
        }

        [Fact]
        public void Convert_EmptyFromUnit_ThrowsArgumentException()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "", ToUnit = "mile" };
            Assert.Throws<ArgumentException>(() => _service.Convert(request));
        }

        [Fact]
        public void Convert_EmptyToUnit_ThrowsArgumentException()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "kilometer", ToUnit = "" };
            Assert.Throws<ArgumentException>(() => _service.Convert(request));
        }

        [Fact]
        public void Convert_UnknownFromUnit_ThrowsArgumentException()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "lightyear", ToUnit = "meter" };
            var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));
            Assert.Contains("lightyear", ex.Message);
        }

        [Fact]
        public void Convert_UnknownToUnit_ThrowsArgumentException()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "meter", ToUnit = "parsec" };
            var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));
            Assert.Contains("parsec", ex.Message);
        }

        [Fact]
        public void Convert_DifferentCategories_ThrowsArgumentException()
        {
            // you can't convert length to weight
            var request = new ConversionRequest { Value = 1, FromUnit = "meter", ToUnit = "kilogram" };
            var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));
            Assert.Contains("category", ex.Message.ToLower());
        }

        [Fact]
        public void Convert_NegativeKelvin_ThrowsArgumentException()
        {
            // below absolute zero doesn't make sense
            var request = new ConversionRequest { Value = -1, FromUnit = "kelvin", ToUnit = "celsius" };
            Assert.Throws<ArgumentException>(() => _service.Convert(request));
        }

        [Fact]
        public void Convert_NaNValue_ThrowsArgumentException()
        {
            var request = new ConversionRequest { Value = double.NaN, FromUnit = "meter", ToUnit = "foot" };
            Assert.Throws<ArgumentException>(() => _service.Convert(request));
        }

        [Fact]
        public void Convert_InfinityValue_ThrowsArgumentException()
        {
            var request = new ConversionRequest { Value = double.PositiveInfinity, FromUnit = "meter", ToUnit = "foot" };
            Assert.Throws<ArgumentException>(() => _service.Convert(request));
        }

        // ===================== RESPONSE METADATA TESTS =====================

        [Fact]
        public void Convert_ResponseContainsCorrectCategory()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "kilometer", ToUnit = "mile" };
            var result = _service.Convert(request);

            Assert.Equal("length", result.Category);
        }

        [Fact]
        public void Convert_ResponseContainsOriginalInputValue()
        {
            var request = new ConversionRequest { Value = 42.5, FromUnit = "kg", ToUnit = "lbs" };
            var result = _service.Convert(request);

            Assert.Equal(42.5, result.InputValue);
        }

        // ===================== VOLUME TESTS =====================

        [Fact]
        public void Convert_LitersToGallons_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 1, FromUnit = "liter", ToUnit = "gallon" };
            var result = _service.Convert(request);

            // 1 liter = 0.264172 US gallons
            Assert.Equal(0.264172, result.ConvertedValue, precision: 4);
        }

        // ===================== SPEED TESTS =====================

        [Fact]
        public void Convert_KphToMph_ReturnsCorrectResult()
        {
            var request = new ConversionRequest { Value = 100, FromUnit = "km/h", ToUnit = "mph" };
            var result = _service.Convert(request);

            // 100 km/h ~= 62.137 mph
            Assert.Equal(62.137, result.ConvertedValue, precision: 2);
        }
    }
}
