internal sealed class Transaction
{
    private readonly Object m_lock = new Object(); // теперь блокирование
                                                    // в рамках каждой транзакции закрыто
    private DateTime m_timeOfLastTrans;

    public void PerformTransaction()
    {
        Console.WriteLine(3);
        Monitor.Enter(this);    // вход в закрытую блокировку
        Console.WriteLine(4);
        // Этот код имеет экслюзивный доступ к данным....
        m_timeOfLastTrans = DateTime.Now;
        Thread.Sleep(1_000);
        Console.WriteLine(5);
        Monitor.Exit(this);     // выход из закрытой блокировки
        Console.WriteLine(6);
    }

    public DateTime LastTransaction
    {
        get
        {
            Console.WriteLine(8);
            Monitor.Enter(this); // вход в закрытую блокировку
            Console.WriteLine(9);
            // Этот код имеет совместный доступ к данным...
            DateTime temp = m_timeOfLastTrans;
            Console.WriteLine(10);
            Monitor.Exit(this);   // выход из закрытой блокировки
            Console.WriteLine(11);
            return temp;
        }
    }
}

