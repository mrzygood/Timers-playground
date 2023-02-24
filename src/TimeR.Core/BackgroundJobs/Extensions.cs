namespace TimeR.Core.BackgroundJobs;

public static class Extensions
{
    public static IServiceCollection AddTimersPerType(this IServiceCollection services)
    {
        services.AddHostedService<ThreadingTimerBackgroundService>();
        // services.AddHostedService<SystemTimerBackgroundService>();
        // services.AddHostedService<PeriodicTimerBackgroundService>();
        
        return services;
    } 
}
