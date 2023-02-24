namespace TimeR.Core.Jobs;

public static class Extensions
{
    public static IServiceCollection AddJobsTimers(this IServiceCollection services)
    {
        services.AddSingleton<IJob, Job1>();
        services.AddSingleton<IJob, Job2>();
        // Uncomment if you want to test throwing exception in method
        // services.AddSingleton<IJob, Job3>();
        services.AddSingleton<IJobsPlanner, JobsPlanner>();
        services.AddHostedService<JobPlannerBackgroundService>();
        return services;
    } 
}
