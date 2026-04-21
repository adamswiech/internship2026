using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Options;

namespace leaderboardServer
{
    public static class HangfireExtensions
    {
        public static void AddHangfireServices(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddHangfire(cfg => cfg.UseSqlServerStorage(connectionString));


            builder.Services.AddHangfireServer(opts => {
                opts.SchedulePollingInterval = TimeSpan.FromSeconds(1);
                opts.Queues = new[] { "leaderboard", "score","default"  };
                opts.WorkerCount = Environment.ProcessorCount * 20;
            });
            
        }

        public static void UseHangfireDashboard(this WebApplication app)
        {
            app.UseHangfireDashboard("/hangfire");
        }
        public static void DeleteAllRecurringJobs(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

            using (var connection = JobStorage.Current.GetConnection())   // This is safe here
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    recurringJobManager.RemoveIfExists(recurringJob.Id);
                }
            }
        }
    }
}