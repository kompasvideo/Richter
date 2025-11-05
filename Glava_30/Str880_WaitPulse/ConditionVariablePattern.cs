namespace Str880_WaitPulse;

internal sealed class ConditionVariablePattern
{
    private readonly Object m_lock = new Object();
    private bool m_condition = false;

    public void Thread1()
    {
        Console.WriteLine("Thread1 - 1");
        Monitor.Enter(m_lock); // Взаимоисключающая блокировка

        Console.WriteLine("Thread1 - 2");
        // "Атомарная" проверка сложного условия блокирования
        while (!m_condition)
        {
            Console.WriteLine("Thread1 - 3");
            // Если условие не соблюдается, ждём, что его поменяет другой поток
            Monitor.Wait(m_lock);   // на время снимаем блокировку,
                                    // чтобы другой поток мог ее получить
            Console.WriteLine("Thread1 - 4");
        }

        Console.WriteLine("Thread1 - 5");
        // Условие соблюдено, обрабатываем данные...
        Monitor.Exit(m_lock);   // Снятие блокировки
        Console.WriteLine("Thread1 - 6");
    }

    public void Thread2()
    {
        Console.WriteLine("Thread2 - 1");
        Monitor.Enter(m_lock);  // Взаимоисключающая блокировка

        Console.WriteLine("Thread2 - 2");
        // Обрабатываем данные и изменяем условие ....
        m_condition = true;

        Console.WriteLine("Thread2 - 3");
        // Monitor.Pulse(m_lock);  // Будим одного ожидающего ПОСЛЕ отмены блокировки
        Monitor.PulseAll(m_lock);  // Будим всех ожидающих ПОСЛЕ отмены блокировки

        Console.WriteLine("Thread2 - 4");
        Monitor.Exit(m_lock);       // Снятие блокировки
        Console.WriteLine("Thread2 - 5");
    }
}
