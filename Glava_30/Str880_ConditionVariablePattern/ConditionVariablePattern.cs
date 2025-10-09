namespace Str880_ConditionVariablePattern;

internal sealed class ConditionVariablePattern
{
    private readonly Object m_lock = new object();
    private bool m_condition = false;

    public void Thread1()
    {
        Console.WriteLine("Thread1 Enter");
        Monitor.Enter(m_lock); // Взаимоисключающая блокировка
        // "Атомарная" проверка сложного условия блокирования
        while (!m_condition)
        {
            // Если условие не соблюдается, ждем, что его поменяет другой поток
            Monitor.Wait(m_lock); // на время снимаем блокировку,
                                // чтобы другой поток мог ее получить
        }
        // Условие соблюдено, обрабатываем данные
        Monitor.Exit(m_lock); // Снятие блокировки
        Console.WriteLine("Thread1 Exit");
    }

    public void Thread2()
    {
        Console.WriteLine("Thread2 Enter");
        Monitor.Enter(m_lock); // Взаимоисключающая блокировка
        // обрабатываем данные и изменяем ксловие
        m_condition = true;
        Console.WriteLine("Thread2 m_condition = true");
        
        // Monitor.Pulse(m_lock); // Будем одного ожидающего ПОСЛЕ отмены блокировки
        Monitor.PulseAll(m_lock); // Будим всех ожидающих ПОСЛЕ отмены блокировки
        
        Monitor.Exit(m_lock); // снятие блокировки
        Console.WriteLine("Thread2 Exit");
    }
}