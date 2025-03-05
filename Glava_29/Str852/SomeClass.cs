namespace Str852
{
public class SomeClass
    {
        private readonly Mutex m_lock = new Mutex();
        public void Metod1(object c)
        {
            Console.WriteLine("Method1 - 0");
            m_lock.WaitOne();
            // Делаем что-то...
            Thread.Sleep(1000);
            Console.WriteLine("Method1 - 1");
            Method2(1);  // Метод Method2, рекурсивно получающий право на блокировку
            Console.WriteLine("Method1 - 2");
            m_lock.ReleaseMutex();
        }

        public void Method2(object o)
        {
            m_lock.WaitOne();
            // Делаем что-то...
            Thread.Sleep(100);
            Console.WriteLine("Method2");
            m_lock.ReleaseMutex();
        }
        public void Dispose() { m_lock.Dispose(); }
    }
}
