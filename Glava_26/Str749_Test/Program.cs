namespace Str749_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread: queuing an asynchronous operation");
            ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
            Console.WriteLine("Main thread: Doing other work here...");
            Thread.Sleep(10_000);
            Console.WriteLine("Hit <Enter> to end this program ...");
            Console.ReadLine();
        }

        private static void ComputeBoundOp(object state)
        {
            Console.WriteLine("Is ComputeBoundOp: state={0}", state);
            Thread.Sleep(1_000);
        }
    }
}
