class Program
{
    static void Main()
    {
        Console.WriteLine("main");
    }

    public static void ConcurentExclusiveSchedulerDemo()
    {
        var cesp = new ConcurrentExclusiveSchedulerPair();
        var tfExclusive = new TaskFactory(cesp.ExclusiveScheduler);
        var tfConcurrent = new TaskFactory(cesp.ConcurrentScheduler);
        for (int operation = 0; operation < 5; operation++)
        {
            var exclusive = operation < 2; // Для демонстрации
            // создаются 2 монопольных и 3 паралельных задания

            (exclusive ? tfExclusive : tfConcurrent).StartNew(
                () => Console.WriteLine("{0} access", 
                    exclusive ? "exclusive" : "concurrent"));
            // здесь выполняется монопольная запись или параллельное чтение
        }
    }
}