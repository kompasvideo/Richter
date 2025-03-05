namespace Str837
{
    internal struct SimpleSpinLock
    {
        private Int32 m_ResourceInUse; // 0 = false(по умолчанию)? 1 = true

        public void Enter()
        {
            while (true)
            {
                // всегда указывать, что ресурс используется
                // если поток переводит его из свободного состояния,
                // вернуть управление
                if (Interlocked.Exchange(ref m_ResourceInUse, 1) == 0) return;
                // Здесь что-то происходит
            }
            //Yield();
            //Sleep();
            //SpinWait(10);
        }

        public void Leave()
        {
            // помечаем ресурс, как свободный
            Volatile.Write(ref m_ResourceInUse, 0);
        }
    }
}
