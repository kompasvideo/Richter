namespace Str757
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
            new Task(ComputeBoundOp, 5).Start();
            Task.Run(() => ComputeBoundOp(5));
            Console.WriteLine("Hello, World!");
        }

        static void ComputeBoundOp(object state)
        {
            Console.WriteLine($"Is ComputeBoundOp state = {state}");
            Thread.Sleep(1_000);
        }
    }
}

// TaskCreationOptions
[Flags, Serializable]
public enum TaskCreationOptions
{
    None = 0x0000, // по умолчанию

    // по возможности скорее
    PreferFairness = 0x0001,

    // создавать потоки более активно
    LongRunning = 0x0002,

    //  присоединять задание к его родителям
    AttachToParent = 0x0004,

    // при присоединени присоединить как обычную
    DenyChildAttach =0x0008,

    // использовать планировщик по умолчания вместо родительского
    HideScheduler = 0x0010,
}
