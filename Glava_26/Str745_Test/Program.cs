namespace Str745_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread: starting other thread");
            Thread t = new Thread(ComputeBoundOp);

            t.IsBackground = true;
            t.Start();

            Console.WriteLine("Returning from Main");
        }

        private static void ComputeBoundOp()
        {
            Thread.Sleep(10_000);
            Console.WriteLine("Returning from Worker");
        }
    }
}
