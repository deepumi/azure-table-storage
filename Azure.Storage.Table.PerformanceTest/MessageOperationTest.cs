using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Table.PerformanceTest.MicrosoftAzureStorage;
using BenchmarkDotNet.Attributes;

namespace Azure.Storage.Table.PerformanceTest
{
    [RankColumn, MemoryDiagnoser]
    public class MessageOperationTest
    {
        private static TableClient _client = TableStorageAccount.Parse(TableConnection.ConnectionString).CreateTableClient();

        [Benchmark]
        public async Task InsertEdmAsyncDemo()
        {
            var entity = new EdmEntity
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = "custom",
                DateProp = DateTimeOffset.UtcNow.DateTime,
                DoubleProperty = 3.14,
                LongProperty = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                IntProperty = 2300,
                ByteTest =  Encoding.UTF8.GetBytes("Hello world")
            };

            await _client.InsertEdmTypeAsync<EdmEntity>(entity);
        } 
        
     
        [Benchmark]
        public async Task MS_InsertEdmAsyncDemo()
        {
            await MessageOperationMsTest.InsertEdmAsync();
        }
    }
}
