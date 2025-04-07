// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class CountdownEvent : IDisposable
{
    public CountdownEvent(Int32 initialCount) { }

    public void Dispose() { }
    public void Reset(Int32 count) { }          // Присвоиваем CurrentCount
                                                // значение count
    public void AddCount(Int32 signalCount) { } // Увеличение CurrentCount
                                                // на signalCount
    public Boolean TryAddCount(Int32 signalCount) { return true; } // Увеличение CurrentCount
                                                                   // на signalCount
    public Boolean Signal(Int32 signalCount) { return true; } // Уменьшение CurrentCount
                                                              // на signalCount
    public Boolean Wait(Int32 millisecondsTimeout, CancellationToken cancellationToken) { return true; }
    public Int32 CurrrentCount { get; }
    public Boolean IsSet { get; }               // true, если
                                                // CurrentCount равно 0
    public WaitHandle WaitHandle { get; }                                                
}