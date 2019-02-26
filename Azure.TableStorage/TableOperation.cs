using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.TableStorage.Http;

namespace Azure.TableStorage
{
    internal sealed class TableOperation
    {
        private readonly TableOperationType _tableOperationType;

        private readonly TableEntity _tableEntity;

        private readonly TableUri _tableUri;

        private HttpMethod HttpMethod
        {
            get
            {
                switch (_tableOperationType)
                {
                    case TableOperationType.Insert: return HttpMethod.Post;
                    case TableOperationType.Delete: return HttpMethod.Delete;
                    case TableOperationType.Get: return HttpMethod.Get;
                    case TableOperationType.Update: return HttpMethod.Put;
                    default: return HttpMethod.Get;
                }
            }
        }

        internal TableOperation(TableEntity entity, TableOperationType tableOperationType)
        {
            _tableEntity = entity;

            _tableOperationType = tableOperationType;

            _tableUri = new TableUri(entity, tableOperationType);
        }

        internal async Task<TableResult<T>> ExecuteAsync<T>(TableCredentials credentials, TableStorageUri storageUri, HttpClientFactory http) where T : TableEntity
        {
            var timeString = DateTimeOffset.UtcNow.UtcDateTime.ToString("R", CultureInfo.InvariantCulture);

            using (var request = new HttpRequestMessage(HttpMethod, storageUri.BuildRequestUri(_tableUri)))
            {
                request.Headers.Add("x-ms-date", timeString);

                request.Headers.Authorization = credentials.AuthorizationHeader(timeString, _tableUri.Uri);

                using (var response = await http.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, new System.Threading.CancellationToken()).ConfigureAwait(false))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return _tableEntity.DeSerialize<T>(stream, response.StatusCode);
                    }
                }
            }
        }

        internal static TableOperation Insert(TableEntity entity) => new TableOperation(entity, TableOperationType.Insert);

        internal static TableOperation Get(TableEntity entity) => new TableOperation(entity, TableOperationType.Get);

        internal static TableOperation Update(TableEntity entity) => new TableOperation(entity, TableOperationType.Update);

        internal static TableOperation Delete(TableEntity entity) => new TableOperation(entity, TableOperationType.Delete);
    }
}