namespace Str848
{
    public sealed class SimpleWaitLock : IDisposable
    {
        private readonly AutoResetEvent m_available;

        public SimpleWaitLock()
        {
            m_available = new AutoResetEvent(true);  // Изначально свободен
        }

        public void Enter()
        {
            // Блокировка на уровне ядра до освобождения ресурса
            m_available.WaitOne();
        }

        public void Leave()
        {
            // Позволяет другому потоку обратиться к ресурсу
            m_available.Set();
        }

        public void Dispose()
        {
            m_available?.Dispose();
        }
    }
}
