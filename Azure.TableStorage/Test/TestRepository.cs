using System.Threading.Tasks;

namespace Azure.TableStorage.Test
{
    internal abstract class BaseRepository
    {
        private static readonly TableClient _client = TableStorageAccount.Parse("").CreateTableClient();

        internal Task<TableResult<T>> Get<T>(T entity) where T : TableEntity
        {
            return _client.ExecuteAsync<T>(TableOperation.Get(entity));
        }
    }
}
