using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;


namespace Str850;
public class Program
{
    public static void Main()
    {
        Int32 x = 0;
        const Int32 iterations = 10_000_000;

        // Сколько времени займёт инкремент x 10 миллионов раз ?
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            x++;
        }
        Console.WriteLine("Increment x: {0:N0}", sw.ElapsedMilliseconds);

        // Сколько времени займёт инкремент x 10 миллионов раз, если
        // добавить вызов ничего не делающего метода ?
        sw.Restart();
        for (int i = 0; i < iterations; i++)
        {
            M();
            x++;
            M();
        }
        Console.WriteLine("Increment x in M: {0:N0}", sw.ElapsedMilliseconds);

        // Сколько времени займёт инкремент x 10 миллионов раз, если
        // добавить вызов неконкунрирующего объекта SimpleSpinLock ?
        SpinLock sl = new SpinLock();
        sw.Restart();
        for (int i = 0; i < iterations; i++)
        {
            Boolean taken = false;
            sl.Enter(ref taken);
            x++;
            sl.Exit();
        }
        Console.WriteLine("Increment x in SpinLock: {0:N0}", sw.ElapsedMilliseconds);

        // Сколько времени займёт инкремент x 10 миллионов раз, если
        // добавить вызов неконкунрирующего объекта SimpleWaitLock ?
        using (SimpleWaitLock swl = new(1))
        {
            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                swl.Enter();
                x++;
                swl.Leave();
            }
            Console.WriteLine("Increment x in SimpleWaitLock: {0:N0}", sw.ElapsedMilliseconds);

        }
    }
 
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void M()
    {
        // Этот метод только возвращяет управление
    }
}