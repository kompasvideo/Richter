namespace Str758
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // создаём задание Task (оно пока не выполняется)
            Task<Int32> t = new Task<Int32>(n => Sum((Int32)n), 1_000_000_000);

            // Можно начинать выполнение задания через некоторое время
            t.Start();

            // можно ожидать завершения задания в явном виде
            t.Wait(); // ПРИМЕЧАНИЕ: существует перегруженная версия, принимающая тайм-аут
                        // CancellationToken
            
            // Получение результата (свойство Result вызывает метод Wait)
            Console.WriteLine("The Sum is:  " + t.Result); // Значение Int32
        }

        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for(;n>0; n--)            
                checked { sum += n; } // при больших n System.OverflowException
            return sum;
        }
    }
}
