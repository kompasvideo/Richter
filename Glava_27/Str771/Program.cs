Int32 sum = 0;
// один поток выполняет всю работу последовательно
for(Int32 i = 0; i < 1_000; i++) DoWork(i);
// Потоки из пула выполняют работу параллельно
Parallel.For(0, 1_000, i => DoWork(i));

List<int> collection = new List<int>(){ 1, 2, 3, 4 };
// один поток выполняет всю работу по очереди
foreach (var item in collection) DoWork(item);
// Потоки из пула выполняют всю работу параллельно
Parallel.ForEach(collection, item => DoWork(item));

//  Один поток выполняет методы по очереди
Method1();
Method1();
Method1();
// Потоки из очереди выполняют работу одновременно
Parallel.Invoke(
    () => Method1(),
    () => Method1(),
    () => Method1());

void DoWork(Int32 i)
{
    sum += i;
}
void Method1() {}