namespace TimeR.Core.Jobs;

public class Job1 : IJob
{
    private static int _counter;
    private static PeriodicTimer _timer;
    
    private Task DoSomethingAsync()
    {
        Console.WriteLine($"[Job1] Counter: {_counter}");
        Thread.Sleep(100);
        _counter++;
        return Task.CompletedTask;
    }

    public async Task PeriodicJob()
    {
        _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
        while (await _timer.WaitForNextTickAsync())
        {
            await DoSomethingAsync();
        }
    }
}
