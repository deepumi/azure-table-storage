using System.Threading.Tasks;
using Azure.TableStorage.PerformanceTest.MicrosoftAzureStorage;
using BenchmarkDotNet.Attributes;

namespace Azure.TableStorage.PerformanceTest
{
    [RankColumn, MemoryDiagnoser]
    public class MessageOperationTest
    {
        private static TableClient _client = TableStorageAccount.Parse(TableConnection.ConnectionString).CreateTableClient();

        [Params(100)]
        public int Iteration;

        [Benchmark]
        public async Task GetAsync()
        {
            var entity = new MessageEntity { PartitionKey = "006c7e09-261b-4081-a021-db8032bcc01b", RowKey = "demo" };
             
            for (int i = 0; i < Iteration; i++)
            {
                var tableResult = await _client.GetAsync<MessageEntity>(entity);
            }
        }

        //public async Task GetAsync_Pagination_All_Test()
        //{
        //    var token = default(TablePaginationToken);

        //    var entity = new MessageEntity();

        //    var entities = new List<MessageEntity>();

        //    var options = new TableQueryOptions { SelectProperties = "Message", Top = 10 }; //optional

        //    do
        //    {
        //        var result = await _client.QueryAsync<MessageEntity>(entity, token, options);

        //       // entities.AddRange(result.Results);

        //        token = result.TablePaginationToken;

        //    } while (token != null);
             
        //}

        [Benchmark]
        public async Task MS_GetAsync()
        {
            for (int i = 0; i < Iteration; i++)
            {
                await MessageOperationMsTest.GetAsync();
            }
        }
    }
}
