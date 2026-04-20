using GameProcessor.Server.Data;
using GameProcessor.Server.Models;

namespace GameProcessor.Server.Jobs
{
    public class ProcessScore
    {
        private readonly DbContext _dbContext;

        public ProcessScore(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ProcessScoreAsync(Game game)
        {
            if (game.PlayerId <= 0)
            {
                return;
            }

            if (game.Score < 0)
            {
                return ;
            }
            

            var createdGame = await _dbContext.AddAsync(game);

            //await Task.Delay(500);
        }
    }
}
