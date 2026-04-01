using Ksef.Models;
using Microsoft.EntityFrameworkCore;


namespace Ksef
{

    internal class AppDbContext : DbContext
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Ksef;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0";

        public DbSet<Faktura> Faktura {  get; set; }
        //public DbSet<Podmiot> Podmioty { get; set; }
        //DbSet<FaWiersz> FaWiersze { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
