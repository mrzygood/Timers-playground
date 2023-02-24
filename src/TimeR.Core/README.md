## Timery

Basic concepts:
- Callback - logic eg. method, run by timers once or on every specified period of time. 
- Interval - time between following callback executions.

In .NET we have two groups of timers: single and multi threaded.
Single threaded timers are available for UI apps (Windows Forms, WPF) while multi threaded are for server hosted apps.
In these document I will focus on the second group used in context of webapi and will be later called timers on general.

Examples and descriptions are for these 3 timers:
- System.Threading.Timer
- System.Timers.Timer
- System.Threading.PeriodicTimer


Multi threaded timers are using thread-pool. Because of that every callback (sometimes called 'method' in these document) can execute on separate thread.
It is related to fact that two or more callbacks (not valid for PeriodicTimer) can run simultaneously in situation when eg. execution of the first callback
takes more time than defined interval. Be cautious and make callback method reentrant (lock free) and thread-safe.

System clock precision is in the range between 10 and 20ms and timers are based on it in basic configuration. 
But it is possible to change it for more precise if required. 
Because of that precision even if you define interval as 1000ms it may be slightly different. 
If you want to read more about it, let's read a research about accuracy of timers available here:
https://www.robofest.net/2019/TimerReport.pdf

What is important and very ofter pointed out in Microsoft documentation, you should always `Dispose` timer when it's job is done.
Class where you are using it should implement `IDisposable` when you want to do this explicitly.
You can also use implicit `using` construction.

### System.Threading.Timer
If you want to run callback only once in last parameter you should pass `Timeout.Infinite` value.
`Change` method modifies `period`.
Even if you called `Dispose`, a callback can runs if it was already enqueued.
To solve it use `Dispose(WaitHandle)` to wait until all callbacks finish execution.

### System.Timers.Timer
It is wrapper on `System.Threading.Timer`. Configuration of this timer is slightly different. 
* Method is run after firing `Elapsed` event and frequency is controlled by `Interval` property.
* You can decide if timer is disabled or enabled using property: `Enabled`. The same can be obtained by `Start` and `Stop` method.
* Method can be called once or many times if you set `autoReset` on `true`.

All exceptions throw in synchronous method executions are suppressed (will change in future).
It looks different in asynchronous method where exceptions are propagated to calling thread.

### System.Threading.PeriodicTime
Method `WaitForNextTickAsync` can have only one consumer.
It works like enabled `auto-reset` in previous timer.
It is safer because next execution doesn't start until current isn't finished.
Good practice is to set `period` to value greater than execution time.
Calling `Dispose` removed enqueued executions. 
If CancellationToken passed to `WaitForNextTickAsync` cancellation is requested it affects only the single wait operation.

### Single thread timers
These timers can be used in UI apps (Windows Forms, WPF).
* System.Windows.Forms.Timer
* System.Web.UI.Timer

##### Source:  
https://learn.microsoft.com



