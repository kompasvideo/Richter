namespace Str857
{
    class AnotherHybridLock : IDisposable
    {
        // Int32 используетм=ся примитивом в пользовательском режиме
        // (методы Interlocked)
        private Int32 m_waiters = 0;

        // AutoResetEvent - примитивная конструкция режима ядра
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

        // Это поле контролирует зацикливание с целью поднять производительность
        private Int32 m_spincount = 4000; // Произвольно выбранное значение

        // Эти поля указывают, какой поток и сколько раз блокируется
        private Int32 m_owingThreadId = 0, m_recursion = 0;

        public void Enter()
        {
            // Если вызывающий поток уже захватил блокировку, увеливаем рекурсивный
            // счётчик на единицу и вернем управление
            Int32 threadId = Thread.CurrentThread.ManagedThreadId;
            if(threadId == m_owingThreadId) { m_recursion++; return; }

            // Вызывающий поток не захватил блокировку, пытаемся получить её
            SpinWait spinWait = new SpinWait();
            for(Int32 spinCount = 0; spinCount < m_spincount; spinCount++)
            {
                // Если блокирование возможно, этот поток блокируется
                // Задаем некторое состояние и возвращяем управление
                if (Interlocked.CompareExchange(ref m_waiters, 1, 0) == 0) goto GotLock;

                // Даем остальным потоком шанс выполниться
                // в надежде на снятие блокировки
                spinWait.SpinOnce();
            }

            // Зацикливание завершено, а блокировка не снята
            // пытаемся ещё раз
            if (Interlocked.Increment(ref m_waiters) > 1)
            {
                // Остальные потоки заблокированы
                m_waiterLock.WaitOne(); // Ожидаем возможность блокирования
                                        // производительность падает
                // Проснувшись, этот поток получает право на блокирование
                // Задаем некоторое состояние и возвращяем управление
            }

        GotLock:
            // Когда поток блокируется, записываем его индентификатор
            // и указываем, что он получил право на блокирование впервые
            m_owingThreadId = threadId; m_recursion = 1;
        }

        public void Leave()
        {
            // Если вызывающий поток не заперт, ошибка
            Int32 threadId = Thread.CurrentThread.ManagedThreadId;
            if (threadId != m_owingThreadId)
                throw new SynchronizationLockException("Lock not owned by calling thread");

            // Уменьшаем на единицу рекурсивный счётчик. Если поток всё ещё 
            // заперт, просто возвращяем управление
            if (--m_recursion > 0) return;

            m_owingThreadId = 0; // Запертых потоков больше нет

            // Если нет других заблокированных потоков, возвращяем управление
            if (Interlocked.Decrement(ref m_waiters) == 0)
                return;

            // Остальные потоки заблокированы, пробуждаем один из них
            m_waiterLock.Set(); // Значительное падение производительности
        }

        public void Dispose() { m_waiterLock.Dispose(); }
    }
}
