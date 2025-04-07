// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

class ReaderWriterLockSlim : IDisposable
{
    public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy) { }

    public void Dispose() { throw new NotImplementedException(); }

    public void EnterReaderLocl() { }
    public Boolean TryEnterReaderLock(Int32 milliecondsTimeout) { return true; }
    public void ExitReadLock() { }

    public void EnterWriteLock() { }
    public Boolean TryExitWriteLock(Int32 millisecondsTimeout) { return true; }
    public void ExitWriteLock() { }

    // Большинство приложений никогда не обращяется к этим свойствам
    public Boolean IsReadLockHeId { get; }
    public Boolean IsWriteLockHeId { get; }
    public Int32 CurrentReadCount { get; }
    public Int32 RecursiveReadCount { get; }
    public Int32 RecursiveWriteCount { get; }
    public Int32 WaitingReadCount { get; }
    public Int32 WaitingWriteCount { get; }
    public LockRecursionPolicy RecursionPolicy { get; }
    // Не показаны члены связанные с преходом от чтения к записи
}