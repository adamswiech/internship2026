using Microsoft.EntityFrameworkCore;


namespace Faktury.models
{
    public class InvoiceContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<TaxSummary> TaxSummaries { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Terms> Terms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;
              Initial Catalog=KSeF;
              Integrated Security=True;
              TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ksef");

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Seller)
                .WithMany()
                .HasForeignKey(i => i.Seller.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Buyer)
                .WithMany()
                .HasForeignKey(i => i.Buyer.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Lines)
                .WithOne()
                .HasForeignKey(l => l.Id);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.TaxSummaries)
                .WithOne()
                .HasForeignKey(t => t.Id);
        }
    }
}
