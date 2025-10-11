using System.Runtime.CompilerServices;

public enum OneManyMode {Exclusive, Shared }

class AsyncOneManyLock
{
    // Не используем readonly с SpinLock
    private SpinLock m_lock = new SpinLock(true);

    private void Lock()
    {
        bool taken = false;
        m_lock.Enter(ref taken);
    }

    private void Unlock()
    {
        m_lock.Exit();
    }

    private int m_state = 0;
    private bool IsFree { get { return m_state == 0; } }
    private bool IsOwnedByWriter {get { return m_state == 1;}}
    private bool IsOwnedByReaders {get {return m_state > 0;}}
    private int AddReaders(int count) { return m_state += count;}
    private int SubtractReader() {return m_state;}
    private void MakeWriter() {m_state = 1;}
    private void MakeFree() {m_state = 0;}
    
    // Для отстуствия конкуренции (с целью улучшения производительности 
    // и сокращения затрат памяти)
    private readonly Task m_noContentionAccessGranter;
    
    // Каждый ожидающий поток записи пробуждается через свой объект
    // TaskCompletionSource, находящийся в очереди

    private readonly Queue<TaskCompletionSource<Object>> m_qWaitingWrites = new Queue<TaskCompletionSource<object>>();
    
    // Все ожидающие потоки чтения пробуждаются по одному
    // объекту TaskCompletionSource
    private TaskCompletionSource<Object> m_waitingReadersSignal = new TaskCompletionSource<Object>();
    private int m_numWaitingReaders = 0;

    public AsyncOneManyLock()
    {
        m_noContentionAccessGranter = Task.FromResult<Object>(null);
    }

    public Task WaitAsync(OneManyMode mode)
    {
        Task accessGranter = m_noContentionAccessGranter; // предпологается отсутствие конкуренции

        Lock();
        switch (mode)
        {
            case OneManyMode.Exclusive:
                if (IsFree)
                    MakeWriter();// Без конкуренции
                else
                {
                    // Конкуренция: ставим в очередь новое задание записи
                    var tcs = new TaskCompletionSource<Object>();
                    m_qWaitingWrites.Enqueue(tcs);
                    accessGranter = tcs.Task;
                }
                break;
            case OneManyMode.Shared:
                if (IsFree || (IsOwnedByReaders && m_qWaitingWrites.Count == 0))
                {
                    AddReaders(1); // Отсутствие конкуренции
                }
                else
                {   // Конкуренция
                    // Увеличиваем количество ожидающих задания чтения
                    m_numWaitingReaders++;
                    accessGranter = m_waitingReadersSignal.Task.ContinueWith(t => t.Result);
                }
                break;
        }
        Unlock();
        
        return accessGranter;
    }

    public void Release()
    {
        TaskCompletionSource<Object> accessGranter = null;
        Lock();
        if (IsOwnedByWriter) MakeFree(); // Ушло задание записи
        else SubtractReader();           // Ушло задание чтения
        
        if (IsFree)
        {
            // Если ресурс свободен, пробудить одно ожидающее задание записи
            // или все задания чтения
            if (m_qWaitingWrites.Count > 0)
            {
                MakeWriter();
                accessGranter = m_qWaitingWrites.Dequeue();
            }
            else if (m_numWaitingReaders > 0)
            {
                AddReaders(m_numWaitingReaders);
                m_numWaitingReaders = 0;
                accessGranter = m_waitingReadersSignal;
                
                // Создание нового объекта TCS для будущих заданий,
                // которым придется ожидать
                m_waitingReadersSignal = new TaskCompletionSource<object>();
            }
        }
        Unlock();
        
        // Пробуждение задания чтения/записи вне блокировки снижает
        // вероятность конкуренции и повышает производительность
        if (accessGranter != null) accessGranter.SetResult(null);
    }
}