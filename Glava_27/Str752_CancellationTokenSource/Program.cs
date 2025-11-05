class CancellationTokenSource : IDisposable
{
    public void Dispose() { }
    public CancellationTokenSource() {}
    public bool IsCancellationRequested { get; }
    public CancellationToken Token { get; }
    public void Cancel(){}
    public void Cancel(bool throwOnFirstException){}
}