namespace Str857
{
    internal class Program
    {
        private static AnotherHybridLock anotherHybridLock;

        static void Main(string[] args)
        {
            Console.WriteLine("AnotherHybridLock");
            anotherHybridLock = new AnotherHybridLock();
            bool v = ThreadPool.QueueUserWorkItem(Method1, 1);
            Task.Run(() => Method1(1));
            Thread.Sleep(3_000);
        }

        static void Method1(object o)
        {
            Console.WriteLine("Method1 - 0");
            anotherHybridLock.Enter();
            Console.WriteLine("Method1 - 1");
            // Делаем что-то...
            Thread.Sleep(1000);
            Console.WriteLine("Method1 - 2");
            anotherHybridLock.Leave();
        }
    }
}
