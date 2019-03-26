using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Table.Extensions;
using Azure.Storage.Table.Http;

namespace Azure.Storage.Table
{
    internal sealed class TableOperation
    {
        private readonly TableOperationType _operationType;

        private readonly ITableEntity _entity;

        private readonly TableUri _tableUri;

        private readonly IDictionary<string, object> _edmTypeEntity;

        private HttpMethod HttpMethod
        {
            get
            {
                switch (_operationType)
                {
                    case TableOperationType.Get: return HttpMethod.Get;
                    case TableOperationType.InsertEdmType:
                    case TableOperationType.Insert: return HttpMethod.Post;
                    case TableOperationType.Update: return HttpMethod.Put;
                    case TableOperationType.Delete: return HttpMethod.Delete;
                    default: return HttpMethod.Get;
                }
            }
        }

        internal TableOperation(ITableEntity entity, TableOperationType operationType) : this(entity, operationType, default, default) { }

        internal TableOperation(ITableEntity entity, TableOperationType operationType, TablePaginationToken token, TableQueryOptions options)
        {
            _entity = entity;

            _operationType = operationType;

            _tableUri = new TableUri(entity, operationType, TableUriQueryBuilder.Build(token, options));

            if(operationType == TableOperationType.InsertEdmType)
            {
                _edmTypeEntity = EntityPropertyBuilder.Build(entity);
            }
        }

        internal async Task<TableResult<T>> ExecuteAsync<T>(TableCredentials credentials, TableStorageUri storageUri) where T : class
        {
            var result = await ExecuteAsyncInternal(credentials, storageUri);

            if (_operationType == TableOperationType.Get)
            {
                return _entity.DeSerialize<T>(result.ResponseStream, result.StatusCode);
            }

            return new TableResult<T>(result.StatusCode);
        }

        internal async Task<TableQueryResult<T>> ExecuteQueryAsync<T>(TableCredentials credentials, TableStorageUri storageUri) where T : class
        {
            var result = await ExecuteAsyncInternal(credentials, storageUri);

            return _entity.DeSerialize<T>(result.ResponseStream, GetPaginationToken(result.Headers));
        }

        internal static TableOperation Insert(ITableEntity entity) => new TableOperation(entity, TableOperationType.Insert);

        internal static TableOperation InsertEdmType(ITableEntity entity) => new TableOperation(entity, TableOperationType.InsertEdmType);

        internal static TableOperation Get(ITableEntity entity, TableQueryOptions options = null, TablePaginationToken token = null) => new TableOperation(entity, TableOperationType.Get, token, options);

        internal static TableOperation Update(ITableEntity entity) => new TableOperation(entity, TableOperationType.Update);

        internal static TableOperation Delete(ITableEntity entity) => new TableOperation(entity, TableOperationType.Delete);

        private async Task<TableResponse> ExecuteAsyncInternal(TableCredentials credentials, TableStorageUri storageUri)
        {
            var time = DateTimeOffset.UtcNow.UtcDateTime.ToString("R", CultureInfo.InvariantCulture);

            using (var request = new HttpRequestMessage(HttpMethod, storageUri.BuildRequestUri(_tableUri)))
            {
                request.Headers.Add("x-ms-date", time);

                request.Headers.Authorization = credentials.AuthorizationHeader(time, _tableUri.Url);

                if (_operationType == TableOperationType.Insert || _operationType == TableOperationType.InsertEdmType || _operationType == TableOperationType.Update)
                {
                    request.Headers.Add("Prefer", "return-no-content");
                    request.Content = _entity.Serialize(_edmTypeEntity);
                }

                if (_operationType == TableOperationType.Update || _operationType == TableOperationType.Delete)
                    request.Headers.Add("If-Match", "*");

                using (var response = await HttpClientFactory.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                    new CancellationToken()).ConfigureAwait(false))
                {
                    using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        if (!response.IsSuccessStatusCode) ThrowHelper.Throw(await stream.StringAsync(), response.StatusCode);

                        if (_operationType == TableOperationType.Get)
                            return new TableResponse(await stream.CopyAsync(), response.StatusCode, response.Headers);

                        return new TableResponse(response.StatusCode);
                    }
                }
            }
        }

        private static TablePaginationToken GetPaginationToken(HttpHeaders headers)
        {
            if (headers.Contains(TableConstants.NextPartitionKey) && headers.Contains(TableConstants.NextRowKey))
            {
                return new TablePaginationToken(headers);
            }

            return default;
        }
    }
}