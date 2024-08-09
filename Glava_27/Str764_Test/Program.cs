namespace Str764_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // task continue sum
        
        }
        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }
            return sum;
        }
    }
}
