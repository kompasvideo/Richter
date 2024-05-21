using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Str751
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Помещяем данные в контекст логического вызова потока метода Main
            CallContext.LogicalSetData("Name", "Jeffrey");

            // Заставляем поток из пула работать
            // Поток из пула имеет доступ к данным контекста логического вызова
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name={0}",
                CallContext.LogicalGetData("Name")));

            // Запрещяем копирование контекста исполнения потока метода Main
            ExecutionContext.SuppressFlow();

            // заставляем поток из пула выполнить работу 
            // Поток из пула Не имеет доступа к данным контекста логического вызова
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name={0}",
                CallContext.LogicalGetData("Name")));

            // Востанавливаем копирование контекста исполнения потока метода Main
            // на случай будущей работы с другими потоками из пула
            ExecutionContext.RestoreFlow();

            Console.ReadLine();
        }
    }
}
