var singleton = Singleton.GetSingleton();

public class Singleton
{
    private readonly static Object m_lock = new Object();
    private static Singleton m_value;

    private Singleton() { }

    public static Singleton GetSingleton()
    {
        if (m_value != null) return m_value;
        
        Monitor.Enter(m_lock);
        if (m_value == null)
        {
            Singleton temp = new Singleton();
            Volatile.Write(ref m_value, temp);
        }
        Monitor.Exit(m_lock);
        return m_value;
    }
}