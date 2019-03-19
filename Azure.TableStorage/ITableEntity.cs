using System.IO;
using System.Net;
using System.Net.Http;

namespace Azure.TableStorage
{
    public interface ITableEntity
    {
        string PartitionKey { get; set; }

        string RowKey { get; set; }

        HttpContent Serialize();

        TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode) where TResult : class; // GET 

        TableQueryResult<TResult> DeSerialize<TResult>(Stream stream, TablePaginationToken paginationToken) where TResult : class; // Collection

        string TableName { get; }
    }
}