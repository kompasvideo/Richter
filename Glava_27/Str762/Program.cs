namespace Str762
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта Task с отложенным запуском
            Task<Int32> t = Task.Run(() => Sum(CancellationToken.None, 1_000));

            // метод ContinueWith возвращяет объект Task, но обычно
            // он не используется
            Task cwt = t.ContinueWith(task => 
                Console.WriteLine($"The sum is {task.Result}"));
            Thread.Sleep(1000);
        }
        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                // Следующая строка приводит к исключению OperationCanceledException
                // при вызове метода Cancel для объекуа CancelllationTokenSource,
                // на который ссылается маркер
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }
    }
}
