namespace Str781_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking status every 2 seconds");
            Status();
            Console.ReadLine();     // Предотвращение завершения потока
        }

        // Методу можно передавать любые параметры на ваше усмотрение
        private static async void Status()
        {
            while (true)
            {
                Console.WriteLine("Checking status at {0}", DateTime.Now);
                // Здесь размещяется код проверки состояния...

                // В конце цикла создаётся 2-секундная задекржка без блокировки потока
                await Task.Delay(2_000); // await ожидает возвращение управления потоком
            }
        }
    }
}
