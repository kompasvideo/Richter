using System;
using System.Threading;

namespace Str850
{
    internal sealed class SimpleWaitLock : IDisposable
    {
        private readonly Semaphore m_AvailableResources;

        public SimpleWaitLock(Int32 maximumCurrentThreads)
        {
            m_AvailableResources = new Semaphore(maximumCurrentThreads, maximumCurrentThreads);  
        }

        public void Enter()
        {
            // Блокировка на уровне ядра до освобождения ресурса
            m_AvailableResources.WaitOne();
        }

        public void Leave()
        {
            // Позволяет другому потоку обратиться к ресурсу
            m_AvailableResources.Release();
        }

        public void Dispose()
        {
            m_AvailableResources?.Close();
        }
    }
}
