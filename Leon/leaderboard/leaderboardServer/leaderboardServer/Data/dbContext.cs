using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using leaderboardServer.Models;

namespace leaderboardServer.Data
{
    public class dbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Top10> Top10 { get; set; }
        public DbSet<Top10snapshot> Top10snapshots { get; set; }
        public DbSet<snapshotEntry> SnapshotEntries { get; set; }
        public DbSet<player> Players { get; set; }
        public dbContext(DbContextOptions<dbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder b)
        {
            b.HasDefaultSchema("dbo");
            b.Entity<Score>(e =>
            {
                e.ToTable("Scores");
                e.HasKey(x => x.id);
                e.HasIndex(x => x.score);
                e.HasIndex(x => x.time);
            });
            b.Entity<Top10>(e =>
            {
                e.ToTable("Top10");
                e.HasKey(x => x.id);
                e.HasOne(x => x.Score)
                     .WithMany()
                     .HasForeignKey(x => x.scoreId)
                     .OnDelete(DeleteBehavior.SetNull);
                e.HasIndex(x => x.rank).IsUnique();
            }
            );
            b.Entity<Top10snapshot>(e =>
            {
                e.ToTable("Top10snapshots");
                e.HasKey(x => x.id);
                e.HasMany(x => x.Entries)
                    .WithOne(x => x.Top10snapshot)
                    .HasForeignKey(x => x.Top10snapshotId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasIndex(x => x.date);

            });
            b.Entity<snapshotEntry>(e =>
            {
                e.ToTable("SnapshotEntries");
                e.HasKey(x => x.id);
                e.HasIndex(x => new { x.Top10snapshotId, x.rank }).IsUnique();
            });
            b.Entity<player>(e =>
            {
                e.ToTable("Players");
                e.HasKey(x => x.id);
                e.HasIndex(x => x.username).IsUnique();
                e.HasIndex(x => x.avgScore);
                e.HasIndex(x => x.highScore);

                e.HasMany(x => x.Scores)
                    .WithOne(x => x.player)
                    .HasForeignKey(x => x.username)
                    .HasPrincipalKey(x => x.username)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
