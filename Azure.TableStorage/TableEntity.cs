using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Azure.TableStorage
{
    public interface ITableEntity
    {
        string PartitionKey { get; set; }

        string RowKey { get; set; }

        HttpContent Serialize();

        TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode, HttpResponseHeaders headers);

        string TableName { get; }
    }
}
