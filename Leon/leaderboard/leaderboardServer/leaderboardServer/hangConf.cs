using System;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using leaderboardServer.Services;

namespace leaderboardServer
{
    public static class HangfireExtensions
    {
        public static void AddHangfireServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true,
                }));
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3});
            builder.Services.AddHangfireServer(options =>
            {
                options.Queues = new[] { "default", "critical" };
            });
        }

        public static void UseHangfireDashboard(this WebApplication app)
        {

            app.UseHangfireDashboard("/hangfire");
        }

        public static void addRecurringJobs(this WebApplication app)
        {
            RecurringJob.AddOrUpdate<leaderboardService>("genTop10", service => service.genTop10(), "*/30 * * * * *");
            RecurringJob.AddOrUpdate<leaderboardService>("snapshotTop10", service => service.snapshotTop10(), "*/5 * * * *");

        }
    }
}
