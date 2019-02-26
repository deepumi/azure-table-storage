using System.Threading.Tasks;
using Azure.TableStorage.Http;

namespace Azure.TableStorage
{
    internal sealed class TableClient
    {
        private readonly TableCredentials _credentials;

        private readonly TableStorageUri _tableStorageUri;

        private readonly HttpClientFactory _http;

        internal TableClient(TableCredentials tableStorageCredentials, TableStorageUri tableStorageUri, HttpClientFactory http)
        {
            _credentials = tableStorageCredentials;
            _tableStorageUri = tableStorageUri;
            _http = http;
        }

        internal async Task<TableResult<T>> ExecuteAsync<T>(TableOperation operation) where T : TableEntity
        {
            return await operation.ExecuteAsync<T>(_credentials, _tableStorageUri, _http);
        }
    }
}
