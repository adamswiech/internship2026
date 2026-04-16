using dataFetchingApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace dataFetchingApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<PersonalDataModel> PersonalDataSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalDataModel>(entity =>
            {
                entity.ToTable("PersonalData", "dbo");
                entity.Property(e => e.firstName).HasColumnName("FirstName");
                entity.Property(e => e.lastName).HasColumnName("LastName");
                entity.Property(e => e.phoneNumber).HasColumnName("PhoneNumber");
                entity.Property(e => e.emailAddress).HasColumnName("EmailAddress");
                entity.Property(e => e.country).HasColumnName("Country");
                entity.Property(e => e.city).HasColumnName("City");
                entity.Property(e => e.postCode).HasColumnName("PostCode");
                entity.Property(e => e.gender).HasColumnName("Gender");
                entity.Property(e => e.age).HasColumnName("Age");
            });
        }
    }
}
