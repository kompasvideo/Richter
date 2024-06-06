namespace Str760
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<Int32> t = new Task<Int32>(() => Sum(cts.Token, 10_000), cts.Token);
            t.Start();
            
            // Позднее отменим CancellationTokenSource, чтобы отменить Task
            cts.Cancel();  // Это ассинхронный запрос, задача уже может быть запущена

            try
            {
                // В случае отмены задания метод Result генерит
                // исключение AgregateException
                Console.WriteLine("The sum is: " + t.Result);  // Значение Int32
            }
            catch(AggregateException x) 
            {
                // Считаем обработанными все объекты OperationCancelException
                // Все остальные исключения попадают в новый объект AggregateException
                // состоящий только из необработанных исключений
                x.Handle(e => e is OperationCanceledException);

                // Строка выполняется, если все исключения уже обработаны
                Console.WriteLine("Sum was canceled");
            }
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
