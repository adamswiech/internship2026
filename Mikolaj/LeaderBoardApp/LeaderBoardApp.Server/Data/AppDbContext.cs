using Microsoft.EntityFrameworkCore;
using LeaderBoardApp.Server.Models;

namespace LeaderBoardApp.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Player> PlayersSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("LeaderBoard", "dbo");
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.playerId).HasColumnName("playerId");
                entity.Property(e => e.score).HasColumnName("score");
                entity.Property(e => e.gameMode).HasColumnName("gameMode");
            });
        }
    }
}
