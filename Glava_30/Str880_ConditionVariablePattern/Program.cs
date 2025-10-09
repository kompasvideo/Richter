using Str880_ConditionVariablePattern;

var conditionVariablePattern = new ConditionVariablePattern();
Task.Run(() => conditionVariablePattern.Thread1());
Thread.Sleep(1000);
var thread = new Thread(conditionVariablePattern.Thread2);
thread.Start();
await Task.Delay(1_000);
Console.WriteLine("Hello, World!");