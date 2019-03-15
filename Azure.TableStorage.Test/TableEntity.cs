using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Azure.TableStorage.Test
{
    public abstract class TableEntity : ITableEntity
    {
        private readonly JsonSerializer _json = new JsonSerializer();

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string TableName { get; } 

        protected TableEntity() { }

        internal TableEntity(string tableName) => TableName = tableName;

        public TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode, HttpResponseHeaders headers)
        {
            if (stream == null || !stream.CanRead) return null;

            using (var reader = new StreamReader(stream))
            {
                using (var json = new JsonTextReader(reader))
                {
                    return new TableResult<TResult>(_json.Deserialize<TResult>(json), statusCode, headers);
                }
            }
        }

        public HttpContent Serialize()
        {
            return new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
        }
    }
}
