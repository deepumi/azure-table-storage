using System;
using System.Collections.Generic;
using System.Text;
using Azure.Storage.Table.PerformanceTest.MicrosoftAzureStorage;
using Newtonsoft.Json;

namespace Azure.Storage.Table.PerformanceTest
{
    class Program
    {

        static void Main()
        {
           //var summary = BenchmarkRunner.Run<MessageOperationTest>();

            //MessageOperationMsTest.InsertAsync(1).GetAwaiter().GetResult();

            //Console.WriteLine("Enter to contirnue");
            //Console.ReadKey();

            //var entity = new MessageEntity { PartitionKey = "006c7e09-261b-4081-a021-db8032bcc01b", RowKey = "demo" };

            //for (int i = 0; i < 10; i++)
            //{
            //    await _client.GetAsync<MessageEntity>(entity);
            //}

            //Console.WriteLine("Done");

           
            var dict = new Dictionary<string, object>
            {
                ["PartitionKey"] = "mypartitionkey",
                ["RowKey"] = "myrowkey",
                ["Age"] = 23
            };

            var x = JsonConvert.SerializeObject(dict);

            MessageOperationMsTest.InsertEdmAsync().GetAwaiter().GetResult();

            new MessageOperationTest().InsertAsyncDemo().GetAwaiter().GetResult();
            Console.WriteLine("DONE");
            Console.ReadKey();
        }
    }


    public class Demo : TableEntity
    {
        public Guid GuidProp { get; set; }
        public DateTime DateProp { get; set; }
        public bool? Test { get; set; }
        public long? LongProperty { get; set; }
        public int? IntProperty { get; set; }
        public double? DoubleProperty { get; set; }
        public byte[] ByteTest { get; set; } = Encoding.UTF8.GetBytes("Hello world");

        public Demo() : base("edmnull")
        {
            
        }
    }

    //public sealed class Json
    //{
    //    public static Json Create()
    //    {
    //        return new Json();
    //    }

    //    public Json Add(string name, string value)
    //    {
    //        return this;
    //    }
    //}
}
