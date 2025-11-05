namespace Str882_SynchronizedQueue;

internal sealed class SynchronizedQueue<T>
{
    private readonly Object m_lock = new Object();
    private readonly Queue<T> m_queue = new Queue<T>();

    public void Enqueue(T item)
    {
        Monitor.Enter(m_lock);

        // После постановки элемента в очередь пробуждаем
        // один/все ожидающие потоки
        m_queue.Enqueue(item);
        Monitor.PulseAll(m_lock);
        Monitor.Exit(m_lock);
    }

    public T Dequeue()
    {
        Monitor.Enter(m_lock);

        // Выполняем цикл, пока очередь не опустеет (условие)
        while (m_queue.Count == 0)
            Monitor.Wait(m_queue);

        // Удаляем элемент из очереди и возвращяем его на обработку
        T item = m_queue.Dequeue();
        Monitor.Exit(m_lock);
        return item;
    }
}
