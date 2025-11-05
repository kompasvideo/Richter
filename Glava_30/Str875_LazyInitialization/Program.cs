var singleton = Singleton.GetSingleton();
var singleton2 = Singleton.GetSingleton();
Console.WriteLine($"{singleton.GetHashCode()}, {singleton2.GetHashCode()}");

class Singleton
{
    private static readonly Object s_lock = new Object();

    // Это поле ссылается на один объект Singleton
    private static Singleton s_value = null;

    // Закрытый конструктор не даёт внешнему коду создавать экземпляры
    private Singleton()
    {
        // Код инициализации объекта Singleton
    }

    // Открытый статический метод, возвращяющий объект Singleton
    public static Singleton GetSingleton()
    {
        // Если объект Singleton уже создан, возвращяем его
        if (s_value != null) return s_value;

        Monitor.Enter(s_lock);  // Если не создан, позволяет одному потоку сделать это
        if (s_value == null)
        {
            // Если объекта всё ещё нет, создаём его
            Singleton temp = new Singleton();

            // Сохраняем ссылку в перемееной s_value (см. обсуждение даллее)
            Volatile.Write(ref s_value, temp);
        }
        Monitor.Exit(s_lock);

        // Возвращяем ссылку на объект Singleton
        return s_value;
    }
}