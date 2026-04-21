using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Storage;
using leaderboardServer;
using Microsoft.EntityFrameworkCore;
using scoreBoard;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddSingleton<ScoreJobManager>();


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",   
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();  
        });
});
builder.AddHangfireServices(connectionString);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowMyOrigin");

app.DeleteAllRecurringJobs();
app.UseHangfireDashboard("/hangfire");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

var scoreManager = new ScoreJobManager(app.Services.GetRequiredService<IRecurringJobManager>());

app.MapControllers();

app.Run();