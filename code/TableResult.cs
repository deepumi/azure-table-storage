using System.Net;

namespace Azure.TableStorage
{
    public sealed class TableResult<T>
    {
        public T Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
