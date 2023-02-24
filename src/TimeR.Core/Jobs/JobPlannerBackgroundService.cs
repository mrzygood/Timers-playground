namespace TimeR.Core.Jobs;

public sealed class JobPlannerBackgroundService : BackgroundService
{
    private readonly IJobsPlanner _jobsPlanner;
    
    public JobPlannerBackgroundService(IJobsPlanner jobsPlanner)
    {
        _jobsPlanner = jobsPlanner;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _jobsPlanner.PlanJobsAsync();
    }
}
