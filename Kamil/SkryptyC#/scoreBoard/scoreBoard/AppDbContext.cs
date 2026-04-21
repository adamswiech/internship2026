using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using scoreBoard.Models;


public class AppDbContext : DbContext
{
    public DbSet<Score> Score { get; set; }       
    public DbSet<Leaderboard> Leaderboard { get; set; }
    public DbSet<Historic> Historic { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


}