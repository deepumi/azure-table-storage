using System;
using BenchmarkDotNet.Running;

namespace Azure.TableStorage.PerformanceTest
{
    class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run<MessageOperationTest>();
 
            Console.WriteLine("Done");

            Console.ReadKey();
        }
    } 
}
