using Str880_WaitPulse;

Console.WriteLine("Thread1");
var condition = new ConditionVariablePattern();

Task.Run(MyThread);
condition.Thread1();

void MyThread()
{
    Console.WriteLine("Thread2");
    Thread.Sleep(1000);
    condition.Thread2();
    Console.WriteLine("End");
}