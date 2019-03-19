using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Azure.TableStorage.Test
{
    public abstract class TableEntity : ITableEntity
    {
        private readonly JsonSerializer _json = new JsonSerializer();

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        [JsonIgnore]
        public string TableName { get; } 

        protected TableEntity() { }

        internal TableEntity(string tableName) => TableName = tableName;

        public TableQueryResult<TResult> DeSerialize<TResult>(Stream stream, TablePaginationToken paginationToken) where TResult : class
        {
            if (stream == null || !stream.CanRead) return null;

            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                using (var reader = new JsonTextReader(sr) { DateParseHandling = DateParseHandling.None })
                {
                    var queryResult = _json.Deserialize<TableEntityCollection<TResult>>(reader);

                    var x = queryResult.Results;

                    return new TableQueryResult<TResult>(x, paginationToken);
                }
            }
        }

        public TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode) where TResult : class
        {
            if (stream == null || !stream.CanRead) return null;

            using (var reader = new StreamReader(stream))
            {
                using (var json = new JsonTextReader(reader) { DateParseHandling = DateParseHandling.None })
                {
                    return new TableResult<TResult>(_json.Deserialize<TResult>(json), statusCode);
                }
            }
        }

        public HttpContent Serialize() => new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
    }
}