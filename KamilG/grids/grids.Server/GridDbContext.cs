using Microsoft.EntityFrameworkCore;
using grids.Server.Models;

namespace grids.Server
{
    public class GridDbContext : DbContext
    {
        public GridDbContext(DbContextOptions<GridDbContext> options) : base(options) { }
        public DbSet<Osoba> Osoba { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Osoba>().ToTable("dane");
        }

    }
}
