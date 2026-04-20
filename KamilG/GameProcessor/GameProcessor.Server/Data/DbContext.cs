using GameProcessor.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameProcessor.Server.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games => Set<Game>();

        public Task<List<Game>> GetAllAsync()
        {
            return Games.AsNoTracking().ToListAsync();
        }

        public async Task<Game> AddAsync(Game game)
        {
            await Games.AddAsync(game);
            await SaveChangesAsync();
            return game;
        }
    }
}
