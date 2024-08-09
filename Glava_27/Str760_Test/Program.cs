namespace Str760_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // task run cancellationToken
            
        }
        private static Int32 Sum(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                // Следующая строка приводит к исключению OperationCanceledException
                // при вызове метода Cancel для объекуа CancelllationTokenSource,
                // на который ссылается маркер
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }
    }
}
