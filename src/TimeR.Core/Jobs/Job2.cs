using System.Timers;
using Timer = System.Timers.Timer;

namespace TimeR.Core.Jobs;

public class Job2 : IJob
{
    private static int _counter;
    private static Timer _timer;
    
    private void DoSomethingAsync(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine($"[Job2] Counter: {_counter}");
        Thread.Sleep(100);
        _counter++;
        if (_counter % 5 == 0)
        {
            Console.WriteLine("[Job2] Throw exception");
            throw new Exception("Job2 fake exception");
        }
    }
    
    public async Task PeriodicJob()
    {
        _timer = new Timer(1000);
        _timer.Enabled = true;
        _timer.AutoReset = true;
        _timer.Elapsed += DoSomethingAsync;
    }
}
