namespace TimeR.Core.Jobs;

class JobsPlanner : IJobsPlanner
{
    private IEnumerable<IJob> Jobs { get; }
    private readonly List<IJob> _jobs = new();

    public JobsPlanner(IEnumerable<IJob> jobs)
    {
        Jobs = jobs;
    }
    
    public async Task PlanJobsAsync()
    {
        foreach (var job in Jobs)
        {
            job.PeriodicJob();
            _jobs.Add(job);
        }
    }
}
