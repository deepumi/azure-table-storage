using System.Threading.Tasks;

namespace Azure.Storage.Table
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

        public Task<TableResult<T>> GetAsync<T>(ITableEntity entity) where T : class => TableOperation.Get(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> GetAsync<T>(ITableEntity entity, string selectProperties) where T : class
        {
            return TableOperation.Get(entity, new TableQueryOptions { SelectProperties = selectProperties }).ExecuteAsync<T>(_credentials, _tableStorageUri);
        }
  
        public Task<TableQueryResult<T>> QueryAsync<T>(ITableEntity entity, TablePaginationToken token, TableQueryOptions options) where T : class
        {
            return TableOperation.Get(entity, options, token).ExecuteQueryAsync<T>(_credentials, _tableStorageUri);
        }

        public Task<TableResult<T>> InsertAsync<T>(ITableEntity entity) where T : class => TableOperation.Insert(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> InsertEdmTypeAsync<T>(ITableEntity entity) where T : class => TableOperation.InsertEdmType(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> UpdateAsync<T>(ITableEntity entity) where T : class => TableOperation.Update(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);

        public Task<TableResult<T>> DeleteAsync<T>(ITableEntity entity) where T : class => TableOperation.Delete(entity).ExecuteAsync<T>(_credentials, _tableStorageUri);
    }
}