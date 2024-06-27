using System.Text;

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
                var childTasks = new[]
                {
                    tf.StartNew(() => Sum(cts.Token, 10_000)),
                    tf.StartNew(() => Sum(cts.Token, 20_000)),
                    tf.StartNew(() => Sum(cts.Token, Int32.MaxValue)) // исключение OverflowException
                }; 
                // Если дочернее задание становиться источником исключения,
                // отменяем все дочерние задания
                for (Int32 task = 0; task < childTasks.Length; task++)
                    childTasks[task].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
                
                // После завершения дочерних заданий получаем максимальное
                // возвращяемое значение и передаем его другому заданию
                // для вывода
                tf.ContinueWhenAll(childTasks, 
                        completedTasks => completedTasks.Where(
                            t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result),
                        CancellationToken.None)
                    .ContinueWith(t => Console.WriteLine("The maximum is: " + t.Result),
                        TaskContinuationOptions.ExecuteSynchronously);
            });
                            
            // После завершения дочерних заданий выводим,
            // в том числе и необработанные исключения
            parent.ContinueWith(p =>
            {
                // Текст помещён в StringBuilder и однократно вызван
                // метод Console.WriteLine просто потому, что это задание
                // может выполняться паралельно с предыдущим,
                // и я не хочу в путаницы в выводимом результате
                StringBuilder sb = new StringBuilder(
                    "The following exception(s) occurred: " + Environment.NewLine);

                foreach (var e in p.Exception.Flatten().InnerExceptions)
                    sb.AppendLine(" " + e.GetType().ToString());
                Console.WriteLine(sb.ToString());
            }, TaskContinuationOptions.OnlyOnFaulted);
            
            // Запуск родительского задания, которое может запускать дочерние
            parent.Start();
        }
        
        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }
    }
}
