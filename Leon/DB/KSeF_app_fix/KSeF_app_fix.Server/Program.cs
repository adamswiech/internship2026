//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//app.UseDefaultFiles();
//app.MapStaticAssets();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.MapFallbackToFile("/index.html");

//app.Run();





using KSeF_app_fix.Server.Data;
using KSeF_app_fix.Server.Models;
using KSeF_app_fix.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KSeF API", Version = "v1" });

    // map classes
    c.MapType<Invoice>(() => new OpenApiSchema() );
    c.MapType<InvoiceLine>(() => new OpenApiSchema());
    c.MapType<Party>(() => new OpenApiSchema());
    c.MapType<Address>(() => new OpenApiSchema());
    c.MapType<ContactInfo>(() => new OpenApiSchema());
    c.MapType<BankAccount>(() => new OpenApiSchema());
    c.MapType<TaxSummary>(() => new OpenApiSchema());
    c.MapType<PaymentInfo>(() => new OpenApiSchema());
    c.MapType<PartialPayment>(() => new OpenApiSchema());
    c.MapType<Settlement>(() => new OpenApiSchema());
    c.MapType<Charge>(() => new OpenApiSchema());
    c.MapType<Deduction>(() => new OpenApiSchema());
    c.MapType<Terms>(() => new OpenApiSchema());
    c.MapType<Contract>(() => new OpenApiSchema());
    c.MapType<OrderInfo>(() => new OpenApiSchema());
    c.MapType<TransportInfo>(() => new OpenApiSchema());
    c.MapType<Carrier>(() => new OpenApiSchema());
});
builder.Services.AddDbContext<KsefContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<InvoiceMapper>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.UseFileServer();

app.Run();



