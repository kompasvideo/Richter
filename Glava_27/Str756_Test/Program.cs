using System.Threading.Channels;

namespace Str756_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // register
            CancellationTokenSource cts1 = new CancellationTokenSource();
            cts1.Token.Register(() => Console.WriteLine("cts1 is canceled"));

            CancellationTokenSource cts2 = new CancellationTokenSource();
            cts2.Token.Register(() => Console.WriteLine("cts2 is canceled"));

            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                cts1.Token, cts2.Token);
            linkedCts.Token.Register(() => Console.WriteLine("cts1 and cts2 is canceled"));

            cts1.Cancel();
            Console.WriteLine($"cts1 is canceled = {cts1.IsCancellationRequested}," +
                $" cts2 is canceled = {cts2.IsCancellationRequested}," +
                $" linkedCts is canceled = {linkedCts.IsCancellationRequested}");
        }
    }
}
