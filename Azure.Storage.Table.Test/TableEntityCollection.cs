using System.Collections.Generic;
using Newtonsoft.Json;

namespace Azure.Storage.Table.Test
{
    public sealed class TableEntityCollection<T>
    {
        [JsonProperty("value")]
        public List<T> Results { get; set; }
    }
}
