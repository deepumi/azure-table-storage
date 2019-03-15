using System.Threading.Tasks;

namespace Azure.TableStorage
{
    public sealed class TableClient
    {
        private readonly TableCredentials _credentials;

        private readonly TableStorageUri _tableStorageUri;

        internal TableClient(TableCredentials tableStorageCredentials, TableStorageUri tableStorageUri)
        {
            _credentials = tableStorageCredentials;
            _tableStorageUri = tableStorageUri;
        }

        public Task<TableResult<T>> GetAsync<T>(ITableEntity entity) => TableOperation.Get(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> GetAsync<T>(ITableEntity entity, string selectProperties)
        {
            return TableOperation.Get(entity, new TableQueryOptions { SelectProperties = selectProperties }).ExecuteAsync<T>(_credentials, _tableStorageUri);
        }

        public Task<TableResult<T>> QueryAsync<T>(ITableEntity entity, TablePaginationToken token, TableQueryOptions options)
        {
            return TableOperation.Get(entity, options, token).ExecuteAsync<T>(_credentials, _tableStorageUri);
        }

        public Task<TableResult<T>> InsertAsync<T>(ITableEntity entity) => TableOperation.Insert(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> UpdateAsync<T>(ITableEntity entity) => TableOperation.Update(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> DeleteAsync<T>(ITableEntity entity) => TableOperation.Delete(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);
    }
}
