using System.Net;
using System.Net.Http.Headers;

namespace Azure.TableStorage
{
    public sealed class TableResult<TResult>
    {
        public TResult Result { get; }

        public HttpStatusCode StatusCode { get; }

        public TablePaginationToken TablePaginationToken { get; }

        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        public TableResult(TResult result, HttpStatusCode statusCode, HttpResponseHeaders headers)
        {
            Result = result;

            StatusCode = statusCode;

            if (headers.Contains(TableConstants.NextPartitionKey) && headers.Contains(TableConstants.NextRowKey))
            {
                TablePaginationToken = new TablePaginationToken(headers);
            }
        }

        internal TableResult(HttpStatusCode statusCode) => StatusCode = statusCode;
    }
}