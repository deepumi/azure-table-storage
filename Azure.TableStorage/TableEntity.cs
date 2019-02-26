using System.IO;
using System.Net;
using System.Net.Http;

namespace Azure.TableStorage
{
    public abstract class TableEntity
    {
        public abstract string PartitionKey { get; set; }

        public abstract string RowKey { get; set; }

        internal abstract string TableName { get; }

        internal virtual string SelectedProperties { get; set; }

        protected TableEntity() { }

        public virtual HttpContent Serialize<T>(T value) => default;
      
        public virtual TableResult<T> DeSerialize<T>(Stream stream, HttpStatusCode statusCode) where T : TableEntity => default;
    }
}
