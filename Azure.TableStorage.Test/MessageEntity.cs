using System.Collections.Generic;
using Newtonsoft.Json;

namespace Azure.TableStorage.Test
{
    public sealed class MessageEntity : TableEntity
    {
        public string Message { get; set; }

        public MessageEntity() : base("DemoTable") { }
    }

    public class MessageEntityCollection
    {
        [JsonProperty("value")]
        public List<MessageEntity> MessageEntityList { get; set; }
    }
}
