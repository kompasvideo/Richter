var singleton = Singleton.GetSingleton();
var singleton2 = Singleton.GetSingleton();
Console.WriteLine($"{singleton.GetHashCode()}, {singleton2.GetHashCode()}");

class Singleton
{
    private static readonly Object s_object = new Object();

    // Это поле ссылается на один объект Singleton
    private static Singleton s_value = null; 

    // Закрытый конструктор не даёт внешнему коду создавать экземпляры
    public static Singleton GetSingleton()
    {
        // Код инициализации объекта Singleton
        if (s_value != null) return s_value;

        Monitor.Enter(s_object);
        if (s_value == null)
        {
            var temp = new Singleton();
            Volatile.Write(ref s_value, temp);
        }
        Monitor.Exit(s_object);
        return s_value;
    }

    // Открытый статический метод, возвращяющий объект Singleton
    
}