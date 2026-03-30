using KSeF.Models;
using Microsoft.EntityFrameworkCore;

namespace KSeF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Faktura> Faktury { get; set; }
        public DbSet<Podmiot> Podmioty { get; set; }
        public DbSet<FaWiersz> Wiersze { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=KSeF;Integrated Security=True;TrustServerCertificate=True;";
            options.UseSqlServer(connectionString);
        }
    }
}
