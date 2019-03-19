using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Azure.TableStorage.PerformanceTest.MicrosoftAzureStorage
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

        internal static async Task GetAsyncAll()
        {
            var table = _tableClient.GetTableReference("DemoTable");
            TableContinuationToken token = null;
            var entities = new List<MessageEntityMs>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<MessageEntityMs>(), token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);
        }
    }

    internal static class TableConnection
    {
        internal static string ConnectionString = "";
    }
}
