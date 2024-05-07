using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Str751_Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CallContext.LogicalSetData("name", "Jeffry");

            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name:{0}",
                CallContext.LogicalGetData("name")));

            ExecutionContext.SuppressFlow();

            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("Name:{0}",
                CallContext.LogicalGetData("name")));

            ExecutionContext.RestoreFlow();
            Console.ReadLine();
        }
    }
}
