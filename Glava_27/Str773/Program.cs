Console.WriteLine("Hello, World!");

public class ParallelOptions
{
    public ParallelOptions() { }
    
    // допускает отмену операций
    public CancellationToken CancellationToken { get; set; }
    // по умолчанию CancellationToken.None
    
    // Позволяет задать максимальное количество рабочих
    // элементов, выполняеммых одновременно
    public Int32 MaxDegreeOfParallelizm { get; set; }
    // По умолчанию -1 (число доступных процессоров)
    
    // Позволяет выбрать планировщика заданий
    public TaskScheduler TaskScheduler { get; set; }
    // По умолчанию TaskScheduler.Default
}