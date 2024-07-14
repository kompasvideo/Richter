using System.Text;

namespace Str767_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Task factory
            
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
