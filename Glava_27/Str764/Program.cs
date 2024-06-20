namespace Str764
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // создание и запуск задания с продолжением
            Task<Int32> t = Task.Run(() => Sum(1000_000));

            // метод ContinueWith возвращяет объект Task, но обычно
            // он не используется
            t.ContinueWith(task => Console.WriteLine("The sum is: " + task.Result),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            t.ContinueWith(task => Console.WriteLine("Sum threw: " + task.Exception),
                TaskContinuationOptions.OnlyOnFaulted);

            t.ContinueWith(task => Console.WriteLine("Sum is canceled"),
                TaskContinuationOptions.OnlyOnCanceled);
            Thread.Sleep(1000);
        }

        private static int Sum(int n)
        {
            Int32 sum = 0;
            for(; n > 0; n--)
                checked { sum += n; }
            return sum;
        }
    }
}
