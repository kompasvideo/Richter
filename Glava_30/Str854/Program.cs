namespace Str854
{
    internal class Program
    {
        private static SimpleHybridLock simpleHybridLock;

        static void Main(string[] args)
        {
            Console.WriteLine("SimpleHybridLock");
            simpleHybridLock = new SimpleHybridLock();
            bool v = ThreadPool.QueueUserWorkItem(Method1, 1);
            Task.Run(() => Method1(1));
            Thread.Sleep(3_000);
        }

        static void Method1(object o)
        {
            Console.WriteLine("Method1 - 0");
            simpleHybridLock.Enter();
            Console.WriteLine("Method1 - 1");
            // Делаем что-то...
            Thread.Sleep(1000);
            Console.WriteLine("Method1 - 2");
            simpleHybridLock.Leave();
        }
    }
}
