using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Azure.Storage.Table.PerformanceTest.MicrosoftAzureStorage
{
    internal static class MessageOperationMsTest
    {
        private static readonly CloudTableClient _tableClient = CloudStorageAccount.Parse(TableConnection.ConnectionString).CreateCloudTableClient();

        internal static async Task GetAsync()
        {
            var entity = new MessageEntityMs { PartitionKey = "006c7e09-261b-4081-a021-db8032bcc01b", RowKey = "demo" };

            var table = _tableClient.GetTableReference("DemoTable");

            var retrieveOperation = TableOperation.Retrieve<MessageEntityMs>(entity.PartitionKey, entity.RowKey);

            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            var x = retrievedResult.Result != null;

            var result = retrievedResult.Result as MessageEntityMs;
        }

        internal static async Task InsertAsync(int i)
        {
            var entity = new MessageEntityMs { PartitionKey = Guid.NewGuid().ToString(), RowKey = "demo", Message = "MS_InsertAsync" };

            var table = _tableClient.GetTableReference("perf");

            var insertOperation = TableOperation.Insert(entity);

            await table.ExecuteAsync(insertOperation);
        }

        internal static async Task InsertEdmAsync()
        {
            var entity = new DemoMs
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = "azure",
                DateProp = DateTimeOffset.UtcNow.DateTime
                //ByteTest = Encoding.UTF8.GetBytes("Hello world"),
                //DoubleProperty = 3.14,
                //LongProperty = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                //IntProperty = 2300
            };

            var table = _tableClient.GetTableReference("edmnull");

            var insertOperation = TableOperation.Insert(entity);

            await table.ExecuteAsync(insertOperation);
        }


        //internal static async Task GetAsyncAll()
        //{
        //    var table = _tableClient.GetTableReference("DemoTable");
        //    TableContinuationToken token = null;
        //    var entities = new List<MessageEntityMs>();
        //    do
        //    {
        //        var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<MessageEntityMs>(), token);
        //        entities.AddRange(queryResult.Results);
        //        token = queryResult.ContinuationToken;
        //    } while (token != null);
        //}
    }

    internal static class TableConnection
    {
        internal static string ConnectionString = "";
    }
}
