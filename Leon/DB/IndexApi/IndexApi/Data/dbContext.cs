using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using IndexApi.Models;

namespace IndexApi.Data
{
    public class dbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder b)
        {
            b.HasDefaultSchema("dbo");
            b.Entity<Person>().ToTable("people").HasKey(x => x.id);

        }
    }
}
