namespace Str764_2_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // child task sum
            

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
