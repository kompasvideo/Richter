namespace Str852
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            SomeClass someClass = new SomeClass();
            bool v = ThreadPool.QueueUserWorkItem(someClass.Metod1, 1);
            Task.Run(() => someClass.Metod1(1));
            Thread.Sleep(3_000);
        }
    }
}
