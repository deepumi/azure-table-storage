using System.Net;

namespace Azure.TableStorage
{
    public sealed class TableResult<TResult> where TResult : class 
    {
        public TResult Result { get; }

        public HttpStatusCode StatusCode { get; }

        public bool IsSuccessStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        public TableResult(TResult result, HttpStatusCode statusCode) : this(statusCode) => Result = result;

        public TableResult(object result, HttpStatusCode statusCode) : this(statusCode) => Result = result as TResult;

        internal TableResult(HttpStatusCode statusCode) => StatusCode = statusCode;
    }
}