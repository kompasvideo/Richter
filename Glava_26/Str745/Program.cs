using System;
using System.Threading;

public static class Program
{
    public static void Main()
    {
        // Создание нового потока (по умолчанию активного)
        Thread t = new Thread(Worker);

        // превращяем роток в фоновый
        t.IsBackground = true;

        t.Start(); // старт потока
        // в случае активного потока приложение будет работать ещё 10 секунд
        // в случае фонового потока приложение немедленно прекратит работу
        Console.WriteLine("Returning from Main");
    }

    private static void Worker()
    {
        Thread.Sleep(10_000); // имитация 10 секунд работы

        // следующая строка выводится только для кода 
        // исполняемого активным потоком
        Console.WriteLine("Returning from Worker");
    }
}