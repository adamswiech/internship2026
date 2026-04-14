using Microsoft.EntityFrameworkCore;
using grids.Server;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<GridDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GridDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}



//var api = app.MapGroup("/api");
//api.MapGet("", () =>
//{

//});

app.MapDefaultEndpoints();
app.MapControllers();
app.UseFileServer();

app.Run();

