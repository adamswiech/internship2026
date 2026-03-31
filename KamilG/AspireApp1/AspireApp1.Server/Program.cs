using Microsoft.EntityFrameworkCore;
using AspireApp1.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add DbContext configuration
builder.Services.AddDbContext<AspireDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Apply migrations automatically
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AspireDbContext>();
        dbContext.Database.Migrate();
    }
}



var api = app.MapGroup("/api");

api.MapGet("/faktury", async (AspireDbContext dbContext) =>
    await dbContext.Faktura
        .Select(f => new
        {
            f.id,
            f.podmiot1Id,
            f.podmiot2Id,
            f.kodWaluty,
            f.p_1,
            f.p_2,
            f.p_6Od,
            f.p_6Do,
            f.p_13_1,
            f.p_14_1,
            f.p_14W,
            f.p_15,
            wierszeCount = f.wiersze.Count
        })
        .ToListAsync());

app.MapDefaultEndpoints();

app.UseFileServer();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
