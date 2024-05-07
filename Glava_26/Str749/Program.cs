
public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Main thread: queuing an asymchronous operation");
        ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
        Console.WriteLine("Main thread: Doing other other work here ...");
        Thread.Sleep(10_000);
        Console.WriteLine("Hit <Enter> to end this program...");
        Console.ReadLine();
    }

    // Сигнатура метода совпадает с сигнатурой делегата WaitCallback
    private static void ComputeBoundOp(object state)
    {
        // метод выполняется потоком из пула
        Console.WriteLine($"Is ComputeBoundOp: state = {state}");
        Thread.Sleep(1_000); // имитация другой работы (1 секунда)
        
        // После возвращения управления методом поток 
        // возвращяется в пул и ожидает следующеего задания
    }
}