namespace Str736
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread: starting a dedicated thread to do an asynchronous operation");
            Thread dedicatedThread = new Thread(ComputeBoundOp);
            dedicatedThread.Start(5);

            Console.WriteLine("Main thread: Doing other work here ...");
            Thread.Sleep(10_000);  // имитация другой работы

            dedicatedThread.Join(); // ожидание завершения потока
            Console.WriteLine("Hit <Enter> to end this program...");
            Console.ReadLine();
        }

        // Сигнатура метода должна совпадать 
        // с сигнатурой  делегата ParametrizedThreadStart
        private static void ComputeBoundOp(Object state)
        {
            // Метод выполняемый выделенным потоком
            Console.WriteLine("In ComputeBoundOp: state={0}", state);
            Thread.Sleep(1_000);

            // После возвращения методом управления выделеный поток завершается
        }
    }
}
