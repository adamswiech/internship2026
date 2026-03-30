using Microsoft.EntityFrameworkCore;
using ksefe.Models;

namespace ksefe.Data
{
    public class KsefeDbContext : DbContext
    {
        public KsefeDbContext(DbContextOptions<KsefeDbContext> options) : base(options)
        {
        }

        public DbSet<podmiot> Podmioty { get; set; }
        public DbSet<faktura> Faktury { get; set; }
        public DbSet<faWiersz> FakturaWiersze { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja podmiot
            modelBuilder.Entity<podmiot>()
                .HasKey(p => p.id);

            // Konfiguracja faktura
            modelBuilder.Entity<faktura>()
                .HasKey(f => f.id);

            modelBuilder.Entity<faktura>()
                .HasOne(f => f.podmiot1)
                .WithMany()
                .HasForeignKey(f => f.podmiot1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<faktura>()
                .HasOne(f => f.podmiot2)
                .WithMany()
                .HasForeignKey(f => f.podmiot2Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<faktura>()
                .HasMany(f => f.wiersze)
                .WithOne(w => w.faktura)
                .HasForeignKey(w => w.fakturaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Konfiguracja faWiersz
            modelBuilder.Entity<faWiersz>()
                .HasKey(w => w.id);
        }
    }
}