using System.Net;

namespace Azure.TableStorage
{
    public sealed class TableResult<T>
    {
        public T Result { get; }

        public HttpStatusCode StatusCode { get;}

        internal TableResult(T result, HttpStatusCode statusCode) 
        {
            Result = result;
            StatusCode = statusCode;
        }
    }
}
