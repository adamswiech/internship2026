using KSeF_appliction.Server.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Faktura> Faktura { get; set; }
    public DbSet<Podmiot> Podmiot { get; set; }
    public DbSet<FaWiersz> FaWiersz { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Podmiot>(entity =>
        {
            entity.ToTable("Podmiot", "ksef");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nip).HasColumnName("Nip");
            entity.Property(e => e.Nazwa).HasColumnName("Nazwa");
            entity.Property(e => e.KodKraju).HasColumnName("KodKraju");
            entity.Property(e => e.AdresL1).HasColumnName("AdresL1");
        });

        modelBuilder.Entity<Faktura>(entity =>
        {
            entity.ToTable("Faktura", "ksef");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Kod_waluty).HasColumnName("KodWaluty");
            entity.Property(e => e.P_13_1).HasColumnName("P_13_1");
            entity.Property(e => e.P_14_1).HasColumnName("P_14_1");
            entity.Property(e => e.P_14_W).HasColumnName("P_14_W");
            entity.Property(e => e.P_15).HasColumnName("P_15");
            entity.Property(e => e.P_1).HasColumnName("P_1");
            entity.Property(e => e.P_2).HasColumnName("P_2");
            entity.Property(e => e.P_6_Od).HasColumnName("P_6_Od");
            entity.Property(e => e.P_6_Do).HasColumnName("P_6_Do");

            entity.HasOne(e => e.podmiot1)
                  .WithMany()
                  .HasForeignKey("Podmiot1Id");

            entity.HasOne(e => e.podmiot2)
                  .WithMany()
                  .HasForeignKey("Podmiot2Id");

            entity.HasMany(e => e.Wiersze)
                  .WithOne()
                  .HasForeignKey("FakturaId")
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<FaWiersz>(entity =>
        {
            entity.ToTable("FaWiersz", "ksef");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nr_wiersza).HasColumnName("NrWiersza");
            entity.Property(e => e.P_9A).HasColumnName("P_9A");
            entity.Property(e => e.P_11).HasColumnName("P_11");
            entity.Property(e => e.P_12).HasColumnName("P_12");
            entity.Property(e => e.Kurs_waluty).HasColumnName("KursWaluty");
        });
    }
}