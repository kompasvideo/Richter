Lazy<string> lazy = new Lazy<string>(
    () => DateTime.Now.ToString(), LazyThreadSafetyMode.None);
Console.WriteLine($"IsValueCreated = {lazy.IsValueCreated}");
Console.WriteLine($"value = {lazy.Value}");
Console.WriteLine($"IsValueCreated = {lazy.IsValueCreated}");
Console.WriteLine($"value = {lazy.Value}");

