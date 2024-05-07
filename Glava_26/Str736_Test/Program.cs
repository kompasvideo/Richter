namespace Str736_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread: starting a dedicated thread to do an asynchronous operation");
            //Thread dedicatedThread = new Thread(() => ComputeBoundOp(4));
            //dedicatedThread.Start();
            Thread dedicatedThread = new Thread(ComputeBoundOp);
            dedicatedThread.Start(5);

            Console.WriteLine("Main thread: Doing other work here...");
            Thread.Sleep(10_000);

            dedicatedThread.Join();
            Console.WriteLine("Hit <Enter> to end this program...");
            Console.ReadLine();
        }

        private static void ComputeBoundOp(object state)
        {
            Console.WriteLine("In ComputeBoundOp: state={0}", state);
            Thread.Sleep(1_000);
        }
    }
}
