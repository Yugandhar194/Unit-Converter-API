using Microsoft.AspNetCore.Mvc;
using UnitConverterAPI.Models;
using UnitConverterAPI.Services;

namespace UnitConverterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;
        private readonly ILogger<ConversionController> _logger;

        public ConversionController(IConversionService conversionService, ILogger<ConversionController> logger)
        {
            _conversionService = conversionService;
            _logger = logger;
        }

        /// <summary>
        /// Converts a value from one unit to another.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/conversion/convert
        ///     {
        ///         "value": 100,
        ///         "fromUnit": "kilometer",
        ///         "toUnit": "mile"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Conversion details including value, source unit, and target unit</param>
        /// <returns>Converted value along with metadata</returns>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(ConversionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Convert([FromBody] ConversionRequest request)
        {
            if (request == null)
                return BadRequest(new { error = "Request body is required." });

            // check model validation - mainly for missing fields
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = _conversionService.Convert(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Conversion failed for request {@Request}: {Message}", request, ex.Message);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // something unexpected happened
                _logger.LogError(ex, "Unexpected error during conversion for request {@Request}", request);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again." });
            }
        }

        /// <summary>
        /// Returns all supported units.
        /// </summary>
        [HttpGet("units")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public IActionResult GetAllUnits()
        {
            var units = _conversionService.GetSupportedUnits();
            return Ok(units);
        }

        /// <summary>
        /// Returns units for a specific category (e.g. length, weight, temperature, volume, speed).
        /// </summary>
        /// <param name="category">Category name</param>
        [HttpGet("units/{category}")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUnitsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return BadRequest(new { error = "Category cannot be empty." });

            var units = _conversionService.GetUnitsByCategory(category);

            if (!units.Any())
                return NotFound(new { error = $"No units found for category '{category}'. Supported categories: length, weight, temperature, volume, speed." });

            return Ok(units);
        }
    }
}
