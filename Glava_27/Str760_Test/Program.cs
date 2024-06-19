namespace Str760_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            Task<Int32> t = new Task<Int32>(() => Sum(cts.Token, 1_000), cts.Token);
            t.Start();
            cts.Cancel();
            try
            {
                Console.WriteLine($"The sum is {t.Result}");
            }
            catch(AggregateException ex)
            {
                ex.Handle( e => e is OperationCanceledException );
                Console.WriteLine("The sum is canceled");
            }
        }

        private static int Sum(CancellationToken ct, int n)
        {
            Int32 sum = 0;
            for(; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }
    }
}
