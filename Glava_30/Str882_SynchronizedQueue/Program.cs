using Str882_SynchronizedQueue;

var sq = new SynchronizedQueue<int>();
Task.Run(() => sq.Enqueue(5));
Task.Run(() => sq.Enqueue(6));

ThreadPool.QueueUserWorkItem(a => sq.Enqueue(7));
//ThreadPool.QueueUserWorkItem((sq.Enqueue, 8);
Console.WriteLine("begin");
Console.WriteLine( sq.Dequeue());
Console.WriteLine( sq.Dequeue());
Console.WriteLine( sq.Dequeue());
Console.WriteLine("Hello, World!");