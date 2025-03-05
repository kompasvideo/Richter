using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Str854
{
    internal class SimpleHybridLock
    {
        // Int32 используется примитивными конструкциями
        // пользовательского режима (Interlocked-методы)
        private Int32 m_waiters = 0;

        // AutoResetEvent - примитивная конструкция режима ядра
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

        public void Enter()
        {
            // Поток хочет получить блокировку
            if (Interlocked.Increment(ref m_waiters) == 1)
                return; // Блокировка свободна, конкуренции нет, возвращяем управление

            // Блокировка захвачена другим потоком (конкуренция),
            // приходится ждать
            m_waiterLock.WaitOne(); // значительное снижение производительности
            // когда WaitOne возвращяет управление, этот поток блокируется
        }

        public void Leave()
        {
            // этот поток освобождает блокировку
            if (Interlocked.Decrement(ref m_waiters) == 0)
                return; // другие потоки не заблокированы, возвращяем управление

            // Другие потоки заблокированы, пробуждаем один из них
            m_waiterLock.Set(); // Значительное снижение производительности
        }

        public void Dispose() { m_waiterLock.Dispose(); }
    }
}
