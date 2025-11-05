namespace Str879_Lazy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание оболочки отложенной инициализайии для получения DateTime
            Lazy<String> s = new Lazy<string> (
                () => DateTime.Now.ToLongTimeString(),
                LazyThreadSafetyMode.PublicationOnly);

            Console.WriteLine(s.IsValueCreated);    // Возвращяется false, так как
                                                    // запроса к Value ещё не было
            Console.WriteLine(s.Value);             // Вызывается этот делегат
            Console.WriteLine(s.IsValueCreated);    // Возвращяет true, так как 
                                                    // был запрос к Value    
            Thread.Sleep(10_000);                   // Теперь делегат НЕ вызывается,
            Console.WriteLine(s.Value);             // результат прежний
        }
    }
}
