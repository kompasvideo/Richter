String name = null;
LazyInitializer.EnsureInitialized(ref name, () => "Jeffrey");
Console.WriteLine(name);

LazyInitializer.EnsureInitialized(ref name, () => "Richter");
Console.WriteLine(name);