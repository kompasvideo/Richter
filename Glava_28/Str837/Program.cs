using Str837;

SimpleSpinLock m_sl = new SimpleSpinLock();
m_sl.Enter();
Console.WriteLine("Hello, World!");
m_sl.Leave();
