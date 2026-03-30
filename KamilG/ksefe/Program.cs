using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ksefe;
using ksefe.Data;

const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=baza;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=SQL Server Management Studio;Command Timeout=0";

var services = new ServiceCollection();

services.AddDbContext<KsefeDbContext>(options =>
    options.UseSqlServer(connectionString));

services.AddScoped<doBazy>();

using var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<KsefeDbContext>();
dbContext.Database.Migrate();

var importer = scope.ServiceProvider.GetRequiredService<doBazy>();
var xmlPath = FindFirstXmlFile();

if (xmlPath is null)
{
    Console.WriteLine("Nie znaleziono pliku XML do importu.");
    return;
}

importer.ImportFromXml(xmlPath);
Console.WriteLine($"Zaimportowano dane z pliku: {xmlPath}");

static string? FindFirstXmlFile()
{
    var folders = new[]
    {
        Directory.GetCurrentDirectory(),
        AppContext.BaseDirectory,
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..")),
        Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."))
    }
    .Distinct()
    .Where(Directory.Exists);

    foreach (var folder in folders)
    {
        var xmlFile = Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly).FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(xmlFile))
        {
            return xmlFile;
        }
    }

    return null;
}