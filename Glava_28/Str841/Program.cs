public class Program
{
    static int begin = 1;
    static void Main()
    {
        Console.WriteLine("begin");
        ThreadPool.QueueUserWorkItem(ThreadMethod, 1);
        ThreadPool.QueueUserWorkItem(ThreadMethod, 3);
        for (int i = 0; i < 1_000_000; i++)
        {
            begin++;
        }
        Console.WriteLine("end");
        Thread.Sleep(1000);
    }

    static Int32 Maximum(ref Int32 target, Int32 value)
    {
        Int32 currentVal = target, startVal, desiredVal;

        // Параметр target может использоваться другим потоком,
        // его трогать не стоит
        do
        {
            // Запись начального значения этой итерации
            startVal = currentVal;

            // Вычисление желаемого значения в контексте startVal и value
            desiredVal = Math.Max(startVal, value);

            // ПРИМЕЧАНИЕ: Здесь поток может быть прерван!

            // if (target == startVal) target = desiredVal;
            // возвращение значения, предшествующего потенциальным изменениям
            currentVal = Interlocked.CompareExchange(ref target, desiredVal, startVal);

            // Если начальное значение на этой итерации изменилось, повторить
        } while (startVal != currentVal);
        return desiredVal;
    }

    static void ThreadMethod(object o)
    {
        int b = 2;
        int res = Maximum(ref begin, b);
        Console.WriteLine(res);
    }
}