using KSeF_App.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace KSeF_App.Server.Data
{
    public class KsefContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<TaxSummary> TaxSummaries { get; set; }
        public DbSet<PaymentInfo> PaymentInfos { get; set; }
        public DbSet<PartialPayment> PartialPayments { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Terms> Terms { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<OrderInfo> Orders { get; set; }
        public DbSet<TransportInfo> TransportInfos { get; set; }
        public DbSet<Carrier> Carriers { get; set; }

        public KsefContext(DbContextOptions<KsefContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder b)
        {
            b.HasDefaultSchema("ksef");

            b.Entity<Invoice>(e =>
            {
                e.ToTable("Invoice");
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Seller)
                    .WithMany()
                    .HasForeignKey(x => x.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Buyer)
                    .WithMany()
                    .HasForeignKey(x => x.BuyerId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.SellerBankAccount)
                    .WithMany()
                    .HasForeignKey("SellerBankAccountId")
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.FactorBankAccount)
                    .WithMany()
                    .HasForeignKey("FactorBankAccountId")
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Payment)
                    .WithOne()
                    .HasForeignKey<PaymentInfo>(p => p.InvoiceId);

                e.HasOne(x => x.Settlement)
                    .WithOne()
                    .HasForeignKey<Settlement>(s => s.InvoiceId);

                e.HasOne(x => x.TransactionTerms)
                    .WithOne()
                    .HasForeignKey<Terms>(t => t.InvoiceId);
            });

            b.Entity<Party>(e =>
            {
                e.ToTable("Party");
                e.HasKey(x => x.Id);

                e.HasOne(x => x.MainAddress)
                    .WithMany()
                    .HasForeignKey(x => x.MainAddressId);

                e.HasOne(x => x.CorrespondenceAddress)
                    .WithMany()
                    .HasForeignKey(x => x.CorrespondenceAddressID);

                e.HasOne(x => x.Contact)
                    .WithMany()
                    .HasForeignKey(x => x.ContactInfoId);
            });

            b.Entity<InvoiceLine>(e =>
            {
                e.ToTable("InvoiceLine");
                e.HasKey(x => x.Id);
                e.Property(x => x.PricePerPiceNetto).HasColumnName("PricePerPieceNetto");

                e.HasOne<Invoice>()
                    .WithMany(i => i.Lines)
                    .HasForeignKey(x => x.InvoiceId);
            });

            b.Entity<TaxSummary>(e =>
            {
                e.ToTable("TaxSummary");
                e.HasKey(x => x.Id);

                e.HasOne<Invoice>()
                    .WithMany(i => i.TaxSummaries)
                    .HasForeignKey(x => x.InvoiceId);
            });

            b.Entity<PaymentInfo>(e =>
            {
                e.ToTable("PaymentInfo");
                e.HasKey(x => x.Id);

                e.HasMany(x => x.PartialPayments)
                    .WithOne(p => p.PaymentInfo)
                    .HasForeignKey(p => p.PaymentInfoId);
            });

            b.Entity<PartialPayment>(e =>
            {
                e.ToTable("PartialPayment");
                e.HasKey(x => x.Id);
            });

            b.Entity<Settlement>(e =>
            {
                e.ToTable("Settlement");
                e.HasKey(x => x.Id);

                e.HasMany(x => x.Charges)
                    .WithOne(c => c.Settlement)
                    .HasForeignKey(c => c.SettlementId);

                e.HasMany(x => x.Deductions)
                    .WithOne(d => d.Settlement)
                    .HasForeignKey(d => d.SettlementId);
            });

            b.Entity<Terms>(e =>
            {
                e.ToTable("Terms");
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Contract)
                    .WithOne()
                    .HasForeignKey<Contract>(c => c.TermsId);

                e.HasOne(x => x.Order)
                    .WithOne()
                    .HasForeignKey<OrderInfo>(o => o.TermsId);

                e.HasOne(x => x.Transport)
                    .WithOne()
                    .HasForeignKey<TransportInfo>(t => t.TermsId);
            });

            b.Entity<Contract>().ToTable("Contract").HasKey(x => x.Id);
            b.Entity<OrderInfo>().ToTable("OrderInfo").HasKey(x => x.Id);

            b.Entity<TransportInfo>(e =>
            {
                e.ToTable("TransportInfo");
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Carrier)
                    .WithMany()
                    .HasForeignKey("CarrierId");

                e.HasOne(x => x.ShipFrom)
                    .WithMany()
                    .HasForeignKey(x => x.ShipFromId);

                e.HasOne(x => x.ShipVia)
                    .WithMany()
                    .HasForeignKey(x => x.ShipViaID);

                e.HasOne(x => x.ShipTo)
                    .WithMany()
                    .HasForeignKey(x => x.ShipToID);
            });

            b.Entity<Carrier>(e =>
            {
                e.ToTable("Carrier");
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Address)
                    .WithMany()
                    .HasForeignKey(x => x.AddressId);
            });

            //b.Entity<Address>(e =>
            //{
            //    e.ToTable("Address");
            //    e.HasKey(x => x.Id);
            //});

            b.Entity<Address>().ToTable("Address");
            b.Entity<ContactInfo>().ToTable("ContactInfo");
            b.Entity<BankAccount>().ToTable("BankAccount");
        }
    }
}