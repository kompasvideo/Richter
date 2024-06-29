using System.Reflection;

Console.WriteLine("Hello, World!");

static void ObsoleteMethods(Assembly assembly)
{
    var query = from type in assembly.GetExportedTypes().AsParallel()
        from method in type.GetMethods(BindingFlags.Public | 
               BindingFlags.Instance | BindingFlags.Static)
        let obsoleteAttrType = typeof(ObsoleteAttribute)
            where Attribute.IsDefined(method, obsoleteAttrType)
            orderby type.FullName
                let obsoleteAttrObj = (ObsoleteAttribute)
                    Attribute.GetCustomAttribute(method, obsoleteAttrType)
                    select String.Format("Type={0},\nMethod={1}\nMessage={2}\n",
                        type.FullName, method.ToString(), obsoleteAttrObj.Message);
                    
                    // вывод результатов
                    foreach(var result in query) Console.WriteLine(result);
}