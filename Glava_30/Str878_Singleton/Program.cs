var singleton = Singleton.GetSingleton();
Console.WriteLine("Hello, World!");

class Singleton
{
    private static Singleton m_value = null;
    private Singleton() {}
    public static Singleton GetSingleton()
    {
        if (m_value != null) return m_value;
        Singleton temp = new Singleton();
        Interlocked.CompareExchange(ref m_value, temp, null);
        return m_value;
    }
}