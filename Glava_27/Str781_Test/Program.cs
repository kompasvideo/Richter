internal static class TimerDemo
{
    private static Timer s_timer;
    public static void Main()
    {
       
    }


    private static void Status(Object state)
    {
        Console.WriteLine("In status at {0}", DateTime.Now);
        Thread.Sleep(1000);
        s_timer?.Change(2000, Timeout.Infinite);
    }
}