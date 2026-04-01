using Microsoft.EntityFrameworkCore;
using AspireApp1.Server.Models;

namespace AspireApp1.Server.Data
{
    public class AspireDbContext : DbContext
    {
        public AspireDbContext(DbContextOptions<AspireDbContext> options) : base(options)
        {
        }

        public DbSet<podmiot> Podmioty { get; set; }
        public DbSet<faktura> Faktura { get; set; }
        public DbSet<faWiersz> FakturaWiersze { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<podmiot>()
                .HasKey(p => p.id);

            modelBuilder.Entity<faktura>()
                .ToTable("Faktury")
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

            modelBuilder.Entity<faWiersz>()
                .HasKey(w => w.id);

            modelBuilder.Entity<podmiot>()
                .HasOne(p => p.podmiot1)
                .WithMany()
                .HasForeignKey(p => p.podmiot1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<podmiot>()
                .HasOne(p => p.podmiot2)
                .WithMany()
                .HasForeignKey(p => p.podmiot2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}