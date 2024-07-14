namespace Str881
{
    internal class Program
    {
        private static Timer s_timer;
        static void Main(string[] args)
        {
            Console.WriteLine("Checking status every 2 seconds");

            // Создание таймера, который никогда не сработывает. Это гарантирует,
            // что ссылка на него будет храниться в s_timer,
            // До активации Status потоком из пула
            s_timer = new Timer(Status, null, Timeout.Infinite, Timeout.Infinite);

            // Теперь, когда s_timer прсвоено значение, можно разрешить таймеру 
            // срабатывать; мы знаем, что вызов Change в Status не выдаст
            // исключение NullReferenceException
            s_timer.Change(0, Timeout.Infinite);

            Console.ReadLine(); // предотвращение завершения процесса
        }

        // Сигнатура этого метода должна соотвествовать
        // сигнатуре делегата TimerCallback
        private static void Status(Object state)
        {
            // Этот метод выполняется поком из пула
            Console.WriteLine("In Status at {0}", DateTime.Now);
            Thread.Sleep(1_000); // Имитация другой работы (1 секунда)

            // Заставляем таймер снова вызывать метод через 2 секунды
            s_timer.Change(2_000, Timeout.Infinite);

            // Когда метод возвращяет управление, поток
            // возвращяется в пул и ожидает следующего задания
        }
    }
}
