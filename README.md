# Unit-Converter-API
A RESTful Unit Converter API built with ASP.NET Core

Project architecture is as follows:

UnitConverterAPI/
├── Controllers/
│   └── ConversionController.cs     # API endpoints
├── Models/
│   ├── ConversionRequest.cs
│   ├── ConversionResponse.cs
│   └── UnitDefinition.cs
├── Services/
│   ├── IConversionService.cs
│   └── ConversionService.cs        # Core conversion logic
├── Data/
│   └── UnitStore.cs                # All unit definitions & factors
├── Program.cs
└── appsettings.json

UnitConverterAPI.Tests/
└── ConversionServiceTests.cs       # 30+ unit tests

UnitConverterAPI

A RESTful Web API built with ASP.NET Core (.NET 8) that converts numerical values between different units of measurement across five categories — Length, Weight, Temperature, Volume, and Speed.


Description
Exposes 3 REST endpoints — convert a value, list all units, or filter units by category
Supports 50+ units across length, weight, temperature, volume, and speed
Accepts full names and abbreviations (kilometer or km, pound or lbs)
Validates all edge cases and returns meaningful 400 Bad Request responses
Swagger UI loads at root (/) — no Postman needed

How to Run Locally
Prerequisites: .NET 8 SDK + VS Code with C# Dev Kit extension

bashgit clone https://github.com/Yugandhar194/unit-converter-api.git
cd unit-converter-api
dotnet restore
cd UnitConverterAPI
dotnet run

Open http://localhost:5000 — Swagger UI loads automatically.

Run tests:

bashcd UnitConverterAPI.Tests
dotnet test


Sample Request & Response

jsonPOST /api/conversion/convert
{
  "value": 100,
  "fromUnit": "kilometer",
  "toUnit": "mile"
}

json{
  "inputValue": 100,
  "fromUnit": "kilometer",
  "convertedValue": 62.1371192,
  "toUnit": "mile",
  "category": "length"
}


Design Decisions & Trade-offs

Base-unit factor approach — each unit holds one multiplier relative to its category's base unit. A→B conversion goes through the base, so adding a new unit is one line in UnitStore.cs rather than N×N pairs. Minimal precision loss, handled with Math.Round(result, 10).
Temperature as a special case — offset-based formulas (not pure multiplication) make a dedicated ConvertTemperature() method the right call. Everything normalises to Celsius first, then converts out.
Static UnitStore over a database — conversion factors are mathematical constants; persistence adds zero value here. Trade-off is a redeploy to add units, which is acceptable given the domain.
Validation in the service, not just the controller — guards against null, empty, NaN, Infinity, unknown units, and cross-category mismatches at the logic layer, so correctness isn't tied to the HTTP pipeline.
IConversionService + DI — decouples the controller from the implementation, enables clean unit testing via mocking, and aligns with standard ASP.NET Core conventions.
