using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Str867_ReaderWriterLockSlim
{
    internal sealed class Transaction : IDisposable
    {
        private readonly ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        private DateTime m_timeOfLastTrans;

        public void PerformWriteLock()
        {
            m_lock.EnterReadLock();
            // Этот код имеет монопольный доступ к данным ....
            m_timeOfLastTrans = DateTime.Now;
            m_lock.ExitWriteLock();
        }

        public DateTime LastTransaction
        {
            get
            {
                m_lock.EnterReadLock();
                // Этот код имеет совместный доступ к данным ....
                DateTime temp = m_timeOfLastTrans;
                m_lock.ExitReadLock();
                return temp;
            }
        }
        public void Dispose() { m_lock.Dispose(); }
    }
}
