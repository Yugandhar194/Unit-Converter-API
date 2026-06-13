using UnitConverterAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// register our conversion service
builder.Services.AddScoped<IConversionService, ConversionService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Unit Converter API",
        Version = "v1",
        Description = "A simple API to convert values between different units of measurement."
    });

    // include XML comments in swagger if they exist
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// always show swagger, even in production for this kind of tool
// in a real app you'd probably only do this in dev
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Unit Converter API v1");
    options.RoutePrefix = string.Empty; // swagger at root
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
