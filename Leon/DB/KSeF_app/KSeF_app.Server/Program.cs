

using KSeF_app.Server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();


builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<KsefContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseFileServer();

app.Run();



