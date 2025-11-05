var singleton = Singleton.GetSingleton();
var singleton2 = Singleton.GetSingleton();
Console.WriteLine($"{singleton.GetHashCode()}, {singleton2.GetHashCode()}");

internal sealed class Singleton
{
    private static Singleton s_value = null;

    // Закрытый конструктор не даёт коду вне данного 
    // класса сохоанять экземпляры
    private Singleton()
    {
        // Код нинициализации объекта Singleton
    }

    // Открытый статический метод, возвращяющий объект Singleton
    // (и создающий его, если это нужно)
    public static Singleton GetSingleton()
    {
        if (s_value != null ) return s_value;
        // Создание нового объекта Singleton и превращение его в корень,
        // если этого ещё не сделал другой поток
        Singleton temp = new Singleton();
        Interlocked.CompareExchange(ref s_value, temp, null);

        // При потере этого потока второй объект Singleton
        // утилизируется сборщиком мусора
        return s_value;     // Воззвращение ссылки на объект
    }
}