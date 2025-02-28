namespace TempClasses;
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}


public sealed class Semaphore : WaitHandle
{
    public Semaphore(Int32 initialCount, Int32 maxCount) { }
    public Int32 Release() { return 0; }

}
