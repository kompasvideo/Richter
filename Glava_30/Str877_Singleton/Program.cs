var singleton = Singleton.GetSingleton();
var singleton2 = Singleton.GetSingleton();
Console.WriteLine($"{singleton.GetHashCode()}, {singleton2.GetHashCode()}");

internal sealed class Singleton
{
    private static Singleton s_value = new Singleton();

    // Закрытый конструктор не даёт коду вне данного класса
    // создавать экземпляры
    private Singleton()
    {
        // Код инициализации объекта Singleton
    }

    // Открытый статический метод, возвращяющий объект Singleton
    // (и создающий его, если этого нужно)
    public static Singleton GetSingleton() { return s_value; }
}