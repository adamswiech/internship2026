using Hangfire;
using LeaderBoardApp.Server.Data;
using LeaderBoardApp.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddHangfire((serviceProvider, configuration) => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseColouredConsoleLogProvider()
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(serviceProvider
        .GetRequiredService<IConfiguration>()
        .GetConnectionString("ConnString")));

builder.Services.AddHangfireServer();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
              new Uri(origin).Host.EndsWith(".dev.localhost"))
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<LeaderBoardService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();

    app.UseHangfireDashboard();
}
else
{
    app.UseExceptionHandler();
}

app.UseCors("AllowFrontend");

app.MapControllers();
app.MapDefaultEndpoints();
app.UseFileServer();
app.Run();