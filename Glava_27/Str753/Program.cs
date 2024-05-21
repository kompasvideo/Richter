namespace Str753
{
    internal static class CancellationDemo
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            // Передаём операции CancellationToken и число
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1_000));

            Console.WriteLine("Press <Enter> to cancel the operation");
            Console.ReadLine();  
            cts.Cancel();   // если метод Count уже вернул управление 
                            // Cancel не оказывает никакого эффекта

            // Cancel немедленно возвращяет управление, метод продолжает работу
            Console.ReadLine();
        }

        private static void Count(CancellationToken token, int countTo)
        {
            for (int count = 0; count < countTo; count++)
            {
                if(token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break; // Выход из цикла для остановки операции
                }

                Console.WriteLine(count);
                Thread.Sleep(100);      // для демонстрационных целей просто ждём                 
            }
            Console.WriteLine("Count is done");
        }
    }
}
