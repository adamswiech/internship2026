using Microsoft.EntityFrameworkCore;

namespace Faktury.Server.models
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<TaxSummary> TaxSummaries { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Terms> Terms { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ksef");

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Seller)
                .WithMany()
                .HasForeignKey(i => i.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Buyer)
                .WithMany()
                .HasForeignKey(i => i.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.Lines)
                .WithOne()
                .HasForeignKey(l => l.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.TaxSummaries)
                .WithOne()
                .HasForeignKey(t => t.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Payment)
                .WithOne()
                .HasForeignKey<PaymentInfo>(p => p.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Settlement)
                .WithOne()
                .HasForeignKey<Settlement>(s => s.InvoiceId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.TransactionTerms)
                .WithOne()
                .HasForeignKey<Terms>(t => t.InvoiceId);
        }
    }
}