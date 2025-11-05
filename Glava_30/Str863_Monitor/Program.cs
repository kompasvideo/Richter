SomeMethod();
 
static void SomeMethod()
{
    var t = new Transaction();
    Console.WriteLine(1);
    Monitor.Enter(t); // Этот поток получает открытую блокировку объекта
    Console.WriteLine(2);
    // Заставляем поток пула вывести время LastTransaction
    // ПРИМЕЧАНИЕ. Поток пула заблокирован до вызова
    // методом SomeMethod метода Monitor.Exit!
    Console.WriteLine(7);
    ThreadPool.QueueUserWorkItem(o => Console.WriteLine(t.LastTransaction));
    Thread.Sleep(5_000);
    Console.WriteLine(12);
    // Здесь выполняется какой-то код....
    Monitor.Exit(t);
    Console.WriteLine(14);
}