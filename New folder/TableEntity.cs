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
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string TableName { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SelectedProperties { get; set; }

        protected TableEntity() { }

        internal TableEntity(string tableName)
        {
            TableName = tableName;
        }

        public TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode, HttpResponseHeaders headers)
        {
            return default;
        }

        public HttpContent Serialize()
        {
            return new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
        }
    }
}
