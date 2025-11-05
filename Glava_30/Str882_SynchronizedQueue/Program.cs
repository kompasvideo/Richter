using Str882_SynchronizedQueue;

Console.WriteLine("Hello, World!");
var synchr = new SynchronizedQueue<int>();
synchr.Enqueue(1);
int i = synchr.Dequeue();
Console.WriteLine(i);