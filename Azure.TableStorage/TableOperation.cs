using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Azure.TableStorage.Extensions;
using Azure.TableStorage.Http;

namespace Azure.TableStorage
{
    internal sealed class TableOperation
    {
        private readonly TableOperationType _operationType;

        private readonly ITableEntity _entity;

        private readonly TableUri _tableUri;

        private HttpMethod HttpMethod
        {
            get
            {
                switch (_operationType)
                {
                    case TableOperationType.Insert: return HttpMethod.Post;
                    case TableOperationType.Delete: return HttpMethod.Delete;
                    case TableOperationType.Get: return HttpMethod.Get;
                    case TableOperationType.Update: return HttpMethod.Put;
                    default: return HttpMethod.Get;
                }
            }
        }

        internal TableOperation(ITableEntity entity, TableOperationType tableOperationType) : this(entity,
            tableOperationType, default, default)
        {
        }

        internal TableOperation(ITableEntity entity, TableOperationType operationType, TablePaginationToken token, TableQueryOptions options)
        {
            _entity = entity;

            _operationType = operationType;

            _tableUri = new TableUri(entity, operationType, TableUriQueryBuilder.Build(token, options));
        }

        internal async Task<TableResult<T>> ExecuteAsync<T>(TableCredentials credentials, TableStorageUri storageUri)
        {
            var timeString = DateTimeOffset.UtcNow.UtcDateTime.ToString("R", CultureInfo.InvariantCulture);

            using (var request = new HttpRequestMessage(HttpMethod, storageUri.BuildRequestUri(_tableUri)))
            {
                request.Headers.Add("x-ms-date", timeString);

                request.Headers.Authorization = credentials.AuthorizationHeader(timeString, _tableUri.Url);

                if (_operationType == TableOperationType.Insert || _operationType == TableOperationType.Update)
                {
                    request.Headers.Add("Prefer", "return-no-content");
                    request.Content = _entity.Serialize();
                }

                if(_operationType == TableOperationType.Update || _operationType == TableOperationType.Delete)
                    request.Headers.Add("If-Match", "*");

                using (var response = await HttpClientFactory.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                    new CancellationToken()).ConfigureAwait(false))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        if (!response.IsSuccessStatusCode) ThrowHelper.Throw(await stream.AsString(), response.StatusCode);

                        if (_operationType == TableOperationType.Get)
                        {
                            return _entity.DeSerialize<T>(stream, response.StatusCode, response.Headers);
                        }

                        return new TableResult<T>(response.StatusCode);
                    }
                }
            }
        }

        internal static TableOperation Insert(ITableEntity entity) => new TableOperation(entity, TableOperationType.Insert);

        internal static TableOperation Get(ITableEntity entity, TableQueryOptions options = null, TablePaginationToken token = null) => new TableOperation(entity, TableOperationType.Get, token, options);

        internal static TableOperation Update(ITableEntity entity) => new TableOperation(entity, TableOperationType.Update);

        internal static TableOperation Delete(ITableEntity entity) => new TableOperation(entity, TableOperationType.Delete);
    }
}