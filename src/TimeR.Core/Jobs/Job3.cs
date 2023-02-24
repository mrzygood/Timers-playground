using Timer = System.Timers.Timer;

namespace TimeR.Core.Jobs;
    
public class Job3 : IJob
{
    private static int _counter;
    private static Timer _timer;
    
    private async Task DoSomethingAsync()
    {
        Console.WriteLine($"[Job3] Counter: {_counter}");
        Thread.Sleep(100);
        _counter++;
        await FakeAsyncMethod();
    }

    private async Task FakeAsyncMethod()
    {
        if (_counter % 5 == 0)
        {
            Console.WriteLine("[Job3] throws exception");
            throw new Exception("Job3 fake exception");
        }

        await Task.CompletedTask;
    }
    
    public async Task PeriodicJob()
    {
        _timer = new Timer(1000);
        _timer.Enabled = true;
        _timer.AutoReset = true;
        _timer.Elapsed += async (_, _) => await DoSomethingAsync();
        // Uncomment if you want to test exception handling
        /*_timer.Elapsed += async (sender, e) =>
        {
            try
            {
                await DoSomethingAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine("*** Exception handled");
            }
        };*/
    }
}
