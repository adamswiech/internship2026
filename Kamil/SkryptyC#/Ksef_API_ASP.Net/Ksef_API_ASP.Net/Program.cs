
using Microsoft.OpenApi;
using Newtonsoft.Json.Linq;
using Sprache;
using Swashbuckle.AspNetCore.Swagger;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

SaveSwaggerJson(app);

app.UseCors("AllowMyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SaveSwaggerJson(WebApplication app)
{
    try
    {
        var swaggerProvider = app.Services.GetRequiredService<ISwaggerProvider>();
        var swaggerDocument = swaggerProvider.GetSwagger("v1");   // Change "v1" if you use a different document name

        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "swagger.json");

        using var stringWriter = new StringWriter();
        var writer = new OpenApiJsonWriter(stringWriter);
        swaggerDocument.SerializeAsV3(writer);   // Use SerializeAsV2 if you need Swagger 2.0

        File.WriteAllText(outputPath, stringWriter.ToString());

        Console.WriteLine($" Swagger JSON successfully saved to: {outputPath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Failed to save swagger.json: {ex.Message}");
    }
}