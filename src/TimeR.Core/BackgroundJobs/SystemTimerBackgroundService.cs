using System.Timers;
using Timer = System.Timers.Timer;

namespace TimeR.Core.BackgroundJobs;

public sealed class SystemTimerBackgroundService : BackgroundService
{
    private readonly Timer _timer;
    private int Counter;
    
    public SystemTimerBackgroundService()
    {
        _timer = new Timer();
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }
    
    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine($"Counter: {Counter}, thread: <{Thread.CurrentThread.ManagedThreadId}>");
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
