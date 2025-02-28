using System.IO.Pipes;
using System.Text;

Console.WriteLine("Hello, World!");

static async Task<String> IssueClientRequestAsync(String serverName, String message)
{
    using (var pipe = new NamedPipeClientStream(serverName, "PipeName",
               PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough))
    {
        pipe.Connect(); // Прежде чем задавать ReadMode, необeходимо
        pipe.ReadMode = PipeTransmissionMode.Message; // вызвать Connect
        
        // Ассинхронная отправка данных серверу
        Byte[] request = Encoding.UTF8.GetBytes(message);
        await pipe.WriteAsync(request, 0, request.Length);
        
        // асинхронное чтение ответа сервера
        Byte[] response = new Byte[1000];
        Int32 bytesRead = await pipe.ReadAsync(response, 0, response.Length);
        return Encoding.UTF8.GetString(response, 0, bytesRead);
    } // закрытие канала
}