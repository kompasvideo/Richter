using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal sealed class Type1 {}
internal sealed class Type2 {}

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
    }
    
    private static async Task<Type1> Method1Async()
    {
        // Ассинхронная операция, возвращающая объект Type1
        return new Type1();
    }
    private static async Task<Type2> Method2Async()
    {
        // Ассинхронная операция, возвращающая объект Type2
        return new Type2();
    }

    // Атрибут AsyncStateMachine обозначает асинхронный метод
    // (полезно для инструментов, использующих отражение)
    // тип указывает, какая структура реализует конечный автомат
    [DebuggerStepThrough, AsyncStateMachine(typeof(StateMachine))]
    private static Task<String> MyMethodAsync(Int32 argument)
    {
        // Создание экземпляра конечного автомата и его инициализация
        StateMachine stateMachine = new StateMachine()
        {
            // создание построителя, возвращающего Task<String>
            // Конечный автомат обращается к построителю для назначения
            // завершения задания или выдачи исключения
            m_builder = AsyncTaskMethodBuilder<String>.Create(),
            m_state = 1, // инициализация местонахождения
            m_argument = argument // копирование аргументов в поле конечного автомата 
        };
        
        // начало выполнения конечного автомата
        stateMachine.m_builder.Start(ref stateMachine);
        return stateMachine.m_builder.Task; // Возвращение задания конечного автомата
    }
    
    // Структура конечного автомата
    [CompilerGenerated, StructLayout(LayoutKind.Auto)]
    private struct StateMachine : IAsyncStateMachine
    {
        // Поле для построителя конечного автомата (Task) и его местонахождения
        public AsyncTaskMethodBuilder<String> m_builder;
        public Int32 m_state;
        
        // аргумент и локальные переменные становятся полями:
        public Int32 m_argument, m_local, m_x;
        public Type1 m_resultType1;
        public Type2 m_resultType2;
        
        // Одно поле на каждый тип Awater
        // В любой момент времени важно только одно из этих полей. В нём 
        // хранится ссылка на последний выполненный элемент await
        // который завершается асинхронно:
        private TaskAwaiter<Type1> m_awaiterType1;
        private TaskAwaiter<Type2> m_awaiterType2;
        
        // Сам конечный автомат
        void IAsyncStateMachine.MoveNext()
        {
            String result = null; // Результат Task
            
            // Вставленный компилятором блок try гарантирует
            // завершение задания конечного автомата
            try
            {
                Boolean executeFinally = true;  // Логический выыход из блока 'try'
                if (m_state == 1)               // Если метод конечного автомата
                {                               // выполняется впервые
                    m_local = m_argument;       // Выполнить начало исходного метода
                }
                // Блок try из исходного кода
                try
                {
                    TaskAwaiter<Type1> awaiterType1;
                    TaskAwaiter<Type2> awaiterType2;

                    switch (m_state)
                    {
                        case 1: // Начало исполнения кода в 'try'
                            //вызвать Method1Async и получить его объект ожидания 
                            awaiterType1 = Method1Async().GetAwaiiter();
                            if (!awaiterType1.IsCompleted)
                            {
                                m_state = 0; // 'Method1Async'
                                // завершается асинхронно
                                m_awaiterType1 = awaiterType1; // Сохранить объект
                                // ожидания до возвращения
                                // Приказать объекту ожидания вызвать MoveNext
                                // после завершения операции
                                m_builder.AwaitUnsafeOnCompleted(ref awaiterType1,
                                    ref this);
                                // предыдущая строка вызывает метод OnCompleted
                                // объекта awaiterType1, что приводит к вызову
                                // ContinueWith(t => MoveNext()) для Task
                                // При завершении Task ContinueWith вызывает MoveNext

                                executeFinally = false; // Без логического выхода
                                // из блока 'try'
                                return; // Поток возващяет 
                            } // управление вызывающей стороне

                            // 'Method1Async' завершается синхронно
                            break;
                        case 0: // 'Method1Async' завершается асинхронно
                            awaiterType1 = m_awaiterType1; // Восстановление последнего
                            break; // объекта ожидания
                        case 2: // 'Method2Async' завершается асинхронно
                            awaiterType2 = m_awaiterType2; // Востановление последнего
                            goto ForLoopEpilog; // объекта ожидания
                    }

                    // после первого await сохраняем результат и запускаем цикл 'for'
                    m_resultType1 = awaiterType1.GetResult(); // 

                    ForLoopPrologue:
                    m_x = 0; // Инициализация цикла 'for'
                    goto ForLoopBody; // Переход к телу цикла 'for'
                    ForLoopEpilog:
                    m_resultType2 = awaiterType2.GetResult();
                    m_x++; // Увеличение x после каждой итерации
                    // Переход к телу цикла 'for'

                    ForLoopBody:
                    if (m_x < 3) // Условие цикла 'for'
                    {
                        // Вызов Method2Async и получение объекта ожидания
                        awaiterType2 = Method2Async().GetAwaiter();
                        if (!awaiterType2.IsCompleted)
                        {
                            m_state = 1; // 'Method2Async' завершается асинхронно
                            m_resultType2 = awaiterType2; // Сохранение объекта
                            // ожидания до возвращения

                            // Приказываем вызвать MoveNext при завершении операции
                            m_builder.AwaitUnsafeOnCompleted(ref awaiterType2,
                                ref this);
                            executeFinally = false; // Без логического выхода
                            // из блока 'try'
                            return; // Поток возвращяет управление
                            // вызывающей стороне
                        }

                        // 'Method2Async' завершается синхронно
                        goto ForLoopEpilog; // Синхронное завершение, возврат
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Catch");
                }
                finally
                {
                    // Каждый раз, когда блок физически выходит из 'try',
                    // выполняется 'finally'
                    // Этот код должен выполниться только при логическом 
                    // выходе из 'try'
                    if (executeFinally)
                        Console.WriteLine("Finally");
                }
                result = "Done";    // То, что в конечном итоге должно вернуть
            }                       // асинхронная функция
            catch (Exception exception)
            {
                // Необработанное исключение: задание конечного автомата
                // завершается с исключением
                m_builder.SetException(exception);
                return;
            }
            // Исключений нет: задание конечного автомата завершается с результатом
            m_builder.SetResult(result);
        }
    }
}

