namespace Str756
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта CancellationTokenSource
            var cts1 = new CancellationTokenSource();
            cts1.Token.Register(() => Console.WriteLine("cts1 canceled"));

            // Создание второго объекта CancellationTokenSource
            var cts2 = new CancellationTokenSource();
            cts2.Token.Register(() => Console.WriteLine("cts2 canceled"));

            // создание нового объекта CancellationTokenSource
            // отменяемого при отмене cts1 и cts2
            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                cts1.Token, cts2.Token);
            linkedCts.Token.Register(() => Console.WriteLine("linked Cancel"));

            // отмена одного из объектов CancellationTokenSource (я выбрал cts2)
            cts2.Cancel();

            // Показываем, какой из объектов CancellationTokenSource был отменён
            Console.WriteLine("cts1 canceled={0}, cts2 canceled={1}, linkedCts={2}",
                cts1.IsCancellationRequested, cts2.IsCancellationRequested, 
                linkedCts.IsCancellationRequested);
        }
    }
}
