namespace Str764_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task<Int32[]> parent = new Task<int[]>(() =>
            {
                var results = new Int32[3]; // Создание массива для результатов

                // Создание и запуск 3 дочерних заданий
                new Task(() => results[0] = Sum(10_000),
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = Sum(20_000),
                    TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = Sum(30_000),
                    TaskCreationOptions.AttachedToParent).Start();

                // Возвращяется ссылка на массив
                // (элементы могут быть не инициализированы)
                return results;
            });

            // вывод результатов после завершения родительского и дочерних заданий
            var cwt = parent.ContinueWith(
                parentTask => Array.ForEach(parentTask.Result,  Console.WriteLine));

            // запуск родительского задания, которое запускает дочернии
            parent.Start();
            Thread.Sleep(1000);
        }

        static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for(; n > 0; n--)
                checked {  sum += n; }
            return sum;
        }
    }
}
