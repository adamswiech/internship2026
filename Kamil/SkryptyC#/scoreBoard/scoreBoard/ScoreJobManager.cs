using Hangfire;
using Microsoft.EntityFrameworkCore;
using scoreBoard.Models;
using System.Linq.Expressions;

namespace scoreBoard
{
    public class ScoreJobManager
    {
        private readonly IRecurringJobManager _recurringJobManager;
        public ScoreJobManager( IRecurringJobManager recurringJobManager)
        {
            _recurringJobManager = recurringJobManager;


            Services services = new Services(new AppDbContext(new DbContextOptions<AppDbContext>()));

            //AddRecurringJob("leaderboard-job","leaderboard",() => services.RecalculateLeaderboard(), "*/5 * * * * *");
            //AddRecurringJob("score-check", () => services.CheckForCheaters(), "*/20 * * * * *");
            //AddRecurringJob("historic-save", "leaderboard",() => services.SaveLeaderBoard(), "*/5 * * * * *");

        }
        public ScoreJobManager(){}

        public void AddRecurringJob(string jobId, Expression<Action> methodCall, string cronExpression)
        {
            _recurringJobManager.AddOrUpdate(jobId, methodCall, cronExpression);
        }

        public void AddRecurringJob(string jobId,string q, Expression<Action> methodCall, string cronExpression)
        {
            _recurringJobManager.AddOrUpdate(jobId, methodCall, cronExpression);
        }
        
        //fire and forget job
        public void AddJob(string q,Expression<Action> methodCall)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue(q,methodCall);
        }
        public void AddJobAndContinueJob(string q,Expression<Action> job, Expression<Action> continueJob)
        {
            var backgroundJobClient = new BackgroundJobClient();
            string id = backgroundJobClient.Enqueue(q,job);
            backgroundJobClient.ContinueWith(id,continueJob);
        }

    }
}
