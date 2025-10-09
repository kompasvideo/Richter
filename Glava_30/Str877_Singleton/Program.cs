var singleton = Singleton.GetSingleton();
Console.WriteLine("Hello, World!");

class Singleton
{
    private static Singleton m_value = new Singleton();
    private Singleton() {}
    public static Singleton GetSingleton()
    {
        return m_value;
    }
}