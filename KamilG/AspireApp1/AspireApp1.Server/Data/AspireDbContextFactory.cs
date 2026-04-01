using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AspireApp1.Server.Data
{
    public class AspireDbContextFactory : IDesignTimeDbContextFactory<AspireDbContext>
    {
        public AspireDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AspireDbContext>();
            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=baza;Initial Catalog=baza;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=SQL Server Management Studio;Command Timeout=0";

            optionsBuilder.UseSqlServer(connectionString);

            return new AspireDbContext(optionsBuilder.Options);
        }
    }
}
