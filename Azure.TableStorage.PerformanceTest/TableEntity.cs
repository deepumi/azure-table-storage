using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Azure.TableStorage.PerformanceTest
{
    public abstract class TableEntity : ITableEntity
    {
        private static readonly JsonSerializer _json = new JsonSerializer();

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        [JsonIgnore]
        public string TableName { get; }

        protected TableEntity() { }

        internal TableEntity(string tableName) => TableName = tableName;

        public TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode) where TResult : class
        {
            if (stream == null || !stream.CanRead) return null;

            using (var sr = new StreamReader(stream,Encoding.UTF8))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    reader.DateParseHandling = DateParseHandling.None;

                    return new TableResult<TResult>(_json.Deserialize<TResult>(reader),statusCode);
                }
            }
        }

        public TableQueryResult<TResult> DeSerialize<TResult>(Stream stream, TablePaginationToken paginationToken) where TResult : class
        {
            if (stream == null || !stream.CanRead) return null;

            using (var sr = new StreamReader(stream))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    return new TableQueryResult<TResult>(_json.Deserialize<MessageList<TResult>>(reader)?.Results, paginationToken);
                }
            }
        }

        public HttpContent Serialize()
        {
            return new StringContent(JsonConvert.SerializeObject(this), Encoding.UTF8, "application/json");
        } 
    }

    public sealed class MessageList<TResult>
    {
        [JsonProperty("value")]
        public List<TResult> Results{ get; set; }
    } 
}
