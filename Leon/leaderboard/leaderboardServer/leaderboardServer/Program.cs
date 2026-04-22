using leaderboardServer;
using leaderboardServer.Data;
using leaderboardServer.Services;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.SqlServer;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();


builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMemoryCache();
builder.Services.AddScoped<leaderboardService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

});



builder.AddHangfireServices();





//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.MapOpenApi(); ;
}
else
{
    app.UseExceptionHandler();
}


app.UseHangfireDashboard("/hangfire");
app.addRecurringJobs();


//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.UseFileServer();

app.Run();







