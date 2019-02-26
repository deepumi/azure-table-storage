using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.TableStorage.Http;

namespace Azure.TableStorage
{
    internal sealed class TableOperation
    {
        private readonly string _timeString = DateTimeOffset.UtcNow.UtcDateTime.ToString("R", CultureInfo.InvariantCulture);

        private readonly TableOperationType _tableOperationType;

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
            //_entity = entity;

            _tableOperationType = tableOperationType;

            _tableUri = new TableUri(entity, tableOperationType);
        }

        internal static TableOperation Insert(TableEntity entity) => new TableOperation(entity, TableOperationType.Insert);

        internal static TableOperation Get(TableEntity entity) => new TableOperation(entity, TableOperationType.Get);

        internal static TableOperation Update(TableEntity entity) => new TableOperation(entity, TableOperationType.Update);

        internal static TableOperation Delete(TableEntity entity) => new TableOperation(entity, TableOperationType.Delete);

        internal async Task<TableResult<T>> ExecuteAsync<T>(TableCredentials credentials, TableStorageUri storageUri, HttpClientFactory http)
        {
            using (var request = new HttpRequestMessage(HttpMethod, storageUri.BuildRequestUri(_tableUri)))
            {
                request.Headers.Add("x-ms-date", _timeString);

                request.Headers.Authorization = TableSignatureBuilder.Create(credentials, BuildSignatureMessage(credentials.AccountName));

                using (var response = await http.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, new System.Threading.CancellationToken()).ConfigureAwait(false))
                {
                    using(var xxx = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                      //  var c = xxx.Deserialize<T>();
                    }
                }
            }

            return default;
        }

        private string BuildSignatureMessage(string accountName) => _timeString + "\n" + "/" + accountName + "/" + _tableUri.Uri;
    }
}