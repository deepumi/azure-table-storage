using System;
using BenchmarkDotNet.Running;

namespace Azure.Storage.Table.PerformanceTest
{
    class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run<MessageOperationTest>();
            Console.WriteLine("DONE");
            Console.ReadKey();
        } 
    }
}