using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ksefe.Data
{
    public class KsefeDbContextFactory : IDesignTimeDbContextFactory<KsefeDbContext>
    {
        public KsefeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KsefeDbContext>();
            const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=baza;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=SQL Server Management Studio;Command Timeout=0";

            optionsBuilder.UseSqlServer(connectionString);

            return new KsefeDbContext(optionsBuilder.Options);
        }
    }
}
