using Ksef.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AppDbContext : DbContext
{
    public DbSet<Faktura> Faktura { get; set; }
    public DbSet<Podmiot> Podmiot{ get; set; }
    public DbSet<FaWiersz> FaWiersz { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<FaWiersz>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FakturaId)
                .IsRequired();

            entity.Property(e => e.NrWiersza)
                .IsRequired();

            //entity.Property(e => e.KursWaluty)
                //.IsRequired()
                //.HasColumnName("KursWaluty")
                //.HasColumnType("decimal(18, 4)");

            entity.Property(e => e.P_7)
                .HasColumnName("P_7");

            entity.Property(e => e.P_8A)
                .HasColumnName("P_8A")
                .HasColumnType("decimal(18, 4)");

            entity.Property(e => e.P_8B)
                .HasColumnName("P_8B")
                .HasColumnType("decimal(18, 4)");

            entity.Property(e => e.P_9A)
                .HasColumnName("P_9A")
                .HasColumnType("decimal(18, 4)");

            entity.Property(e => e.P_11)
                .HasColumnName("P_11")
                .HasColumnType("decimal(18, 4)");

            entity.Property(e => e.P_12)
                .HasColumnName("P_12")
                .HasColumnType("decimal(18, 4)");

            entity.ToTable("FaWiersz");
        });

        modelBuilder.Entity<Faktura>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.KodWaluty)
                .HasColumnName("KodWaluty");

            entity.Property(e => e.KursWaluty)
                .HasColumnName("KursWaluty")
                .HasColumnType("decimal(18, 4)");

            entity.HasMany(e => e.FaWiersze)
                .WithOne()
                .HasForeignKey(e => e.FakturaId);

            entity.ToTable("Faktura");
        });

        modelBuilder.Entity<Podmiot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Podmiot");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);

        optionsBuilder.UseSqlServer("your-connection-string");
    }
}