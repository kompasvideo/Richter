namespace Lazy;
public class Lazy<T>
{
    public Lazy(Func<T> valueFactoty, LazyThreadSafetyMode mode) { }
    public bool IsValueCreated { get; }
    public T Value { get; }
}