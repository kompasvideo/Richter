namespace MyLazyInitializer;
static class LazyInitializer
{
    public static T EnsureInitialized<T>(ref T target) where T : class { return target; }
    public static T EnsureInitialized<T>(ref T target, Func<T> falueFactory) where T : class { return target; }
    public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref Object syncLock) { return target; }
    public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref Object syncLock, Func<T> valueFactory) { return target; }
}