using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Str885_AsyncOneManyLock;

public enum OneManyMode { Exclusive, Shared }
public sealed class AsyncOneManyLock
{
    #region Lock code
    private SpinLock m_lock = new SpinLock();   // не используем
                                                // readonly с SpinLock
    private void Lock() { Boolean taken = false; m_lock.Enter(ref taken); }
    private void Unlock() { m_lock.Exit(); }
    #endregion

    #region Lock state and helper methods
    private Int32 m_state = 0;
    private Boolean IsFree { get { return m_state == 0; } }
    private Boolean IsOwnedByWriter { get { return m_state == 1; } }
    private Boolean IsOwnedByReadeer { get { return m_state > 0; } }
    private Int32 AddReaders(Int32 count) { return m_state += count; }
    private Int32 SubstractReader() { return m_state; }
    private void MakeWriter() { m_state = 1; }
    private void MakeFree() { m_state = 0; }
    #endregion

    // Для отсутствия конкуренции (с целью улучшения производительности
    // и сокращения затрат памяти)
    private readonly Task m_noContentionAccessGranter;

    // Каждый ожидающий поток записи пробуждается через свой объект
    // TaskCompletionSource, находящийся в очереди

    private readonly Queue<TaskCompletionSource<Object>> m_qWaitingWritters
        = new Queue<TaskCompletionSource<Object>>();

    // Все ожидающие потоки чтения пробуждаются по одному
    // объекту TaskCompletionSource
    private TaskCompletionSource<Object> m_waitingReaderSignal
        = new TaskCompletionSource<Object>();
    private Int32 m_numWaitingReaders = 0;

    public AsyncOneManyLock()
    {
        m_noContentionAccessGranter = Task.FromResult<Object>(null);
    }

    public Task WaitAsync(OneManyMode mode)
    {
        Task accressGranter = m_noContentionAccessGranter; // Предпологается
                                                           // отсутствие конкуренции
        Lock();
        switch (mode)
        {
            case OneManyMode.Exclusive:
                if (IsFree)
                    MakeWriter(); // Без конкуренции
                else
                {
                    // Конкуренция: ставим в очередь новое задание записи
                    var tcs = new TaskCompletionSource<Object>();
                    m_qWaitingWritters.Enqueue(tcs);
                    accressGranter = tcs.Task;
                }
                break;

            case OneManyMode.Shared:
                if (IsFree || (IsOwnedByReadeer && m_qWaitingWritters.Count == 0))
                    AddReaders(1); // Отсутствие конкуренции
                else // Конкуренция
                {
                    // Увеличиваем количество ожидающих заданий чтения
                    m_numWaitingReaders++;
                    accressGranter = m_waitingReaderSignal.Task.ContinueWith(t => t.Result);
                }
                break;
        }
        Unlock();

        return accressGranter;
    }

    public void Release()
    {
        TaskCompletionSource<Object> accessGranter = null;

        Lock();
        if (IsOwnedByWriter) MakeWriter();   // Ушло задание записи
        else SubstractReader();              // Ушло задание чтения
        
        if (IsFree)
        {
            // Если ресурс свободен, пробудить одно ожидающее задание записи
            // или все задания чтения
            if (m_qWaitingWritters.Count > 0)
            {
                MakeWriter();
                accessGranter = m_qWaitingWritters.Dequeue();
            }
            else if (m_numWaitingReaders > 0)
            {
                AddReaders(m_numWaitingReaders);
                m_numWaitingReaders = 0;
                accessGranter = m_waitingReaderSignal;

                // Создание нового объекта TCS для будущих заданий,
                // которым придётся ожидать
                m_waitingReaderSignal = new TaskCompletionSource<Object>();
            }
        }
        Unlock();

        // Пробуждение задания чтения/записи вне блокировки снижает
        // вероятность конкуренции и повышает производительность
        if(accessGranter != null) accessGranter.SetResult(null); 
    }
}