namespace TimeR.Core.BackgroundJobs;

public sealed class ThreadingTimerBackgroundService : BackgroundService
{
    private readonly Timer _timer;
    private int Counter;
    
    public ThreadingTimerBackgroundService()
    {
        _timer = new Timer(OnTimedEvent, "test " + Counter, 0, 1000);
    }
    
    private void OnTimedEvent(object? something)
    {
        Console.WriteLine($"Passed value: {something}, counter: {Counter}, thread: <{Thread.CurrentThread.ManagedThreadId}>");
        // Uncomment for simultaneously execution case testing
        // Thread.Sleep(3100);
        Counter += 1;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
