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
