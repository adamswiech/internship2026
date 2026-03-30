using KSeF.Models;
using Microsoft.EntityFrameworkCore;

namespace KSeF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Faktura> Faktura { get; set; }
        //public DbSet<Podmiot> Podmioty { get; set; }
        //public DbSet<FaWiersz> FaWiersze { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=KSeF;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Application Name=\"SQL Server Management Studio\";Command Timeout=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ksef");

            modelBuilder.Entity<Podmiot>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Faktura>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Kod_waluty).HasColumnName("KodWaluty");
                e.Property(x => x.P_14_W).HasColumnType("decimal(18,2)");

                e.HasOne(x => x.podmiot1)
                 .WithMany()
                 .HasForeignKey(x => x.Podmiot1Id)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.podmiot2)
                 .WithMany()
                 .HasForeignKey(x => x.Podmiot2Id)
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(x => x.Wiersze)
                 .WithOne()
                 .HasForeignKey(x => x.FakturaId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FaWiersz>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Nr_wiersza).HasColumnName("NrWiersza");
                e.Property(x => x.Kurs_waluty).HasColumnName("KursWaluty");
            });
        }
    }
}