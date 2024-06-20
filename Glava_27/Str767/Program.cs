namespace Str767
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task parent = new Task(() => {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<Int32>(cts.Token,
                    TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);

                // Задание создаёт и запускает 3 дочерних задания

            });
            Console.WriteLine("Hello, World!");
        }
        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
                checked { sum += n; }
            return sum;
        }
    }
}
