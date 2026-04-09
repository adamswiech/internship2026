using Microsoft.EntityFrameworkCore;
using Ksef.Models;

public class AppDbContext : DbContext
{
    //public DbSet<Faktura> Faktura { get; set; }
    public DbSet<Podmiot> Podmiot{ get; set; }
    public DbSet<FaWiersz> FaWiersz { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

}