namespace TimeR.Core.StandardTimer;

public sealed class PeriodicTimerBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _timer;
    private int Counter;
    
    public PeriodicTimerBackgroundService()
    {
        _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            Counter += 1;
            Console.WriteLine($"Counter: {Counter}, thread: <{Thread.CurrentThread.ManagedThreadId}>");
            await Task.Delay(3000, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Dispose();
        return base.StopAsync(cancellationToken);
    }
}
