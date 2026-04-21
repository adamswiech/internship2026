using Hangfire;
using LeaderBoardApp.Server.Data;
using LeaderBoardApp.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
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


//Hangfire 
builder.Services.AddScoped<LeaderBoardService>();
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<LeaderBoardService>(
        "fetch-scores-every-30s",
        service => service.FetchScores(),
        "*/30 * * * * *"
    );
}

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