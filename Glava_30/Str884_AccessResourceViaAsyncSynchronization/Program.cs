class Program
{
    static void Main()
    {
        Console.WriteLine("main");
    }
    private static async Task AccessResourceViaAsyncSynchronization(SemaphoreSlim asyncLock)
    {
        // разместите здесь любой код на ваше усмотрение
        
        await asyncLock.WaitAsync(); // Запрос монопольного доступа к ресурсу
        // Когда управление попадет в эту точку, мы знаем, что никакой другой
        // поток не может обратиться к ресурсу
        
        // работа с ресурсом в монопольном режиме
        
        // Завершив работу с ресурсом, снимаем блокировку, чтобы ресурс
        // стал доступен другим потокам
        asyncLock.Release();
        
        // разместите здесь любой код на ваше усмотрение
    }
}