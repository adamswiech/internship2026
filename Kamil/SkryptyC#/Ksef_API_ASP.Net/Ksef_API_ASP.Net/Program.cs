
using Microsoft.OpenApi;
using Newtonsoft.Json.Linq;
using Sprache;
using Swashbuckle.AspNetCore.Swagger;

UpdatePort();

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

static void UpdatePort()
{

    string envPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ".env");
    DotNetEnv.Env.Load(envPath);

    var port = Environment.GetEnvironmentVariable("PORTAPI") ?? "8080";
    var host = Environment.GetEnvironmentVariable("HOSTAPI") ?? "localhost";

    var json = File.ReadAllText(Path.Combine("Properties","launchSettings.json"));
    var obj = JObject.Parse(json);

    obj["profiles"]["https"]["applicationUrl"] = $"https://{host}:{port}";

    File.WriteAllText(Path.Combine("Properties", "launchSettings.json"), obj.ToString());
}

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