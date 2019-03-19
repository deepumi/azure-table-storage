namespace Azure.TableStorage.PerformanceTest
{
    public sealed class MessageEntity : TableEntity
    {
        public string Message { get; set; }

        public MessageEntity() : base("DemoTable") { }

        //public override TableResult<TResult> DeSerialize<TResult>(Stream stream, HttpStatusCode statusCode) 
        //{
        //    if (stream == null || !stream.CanRead) return default;

        //    using (var sr = new StreamReader(stream, Encoding.UTF8))
        //    {
        //        using (var reader = new JsonTextReader(sr))
        //        {
        //            while (reader.Read())
        //            {
        //                if (reader.Value == null || reader.Path != "Message" || reader.TokenType == JsonToken.PropertyName) continue;

        //                if (reader.TokenType == JsonToken.String)
        //                {
        //                    return new TableResult<TResult>(new MessageEntity
        //                    {
        //                        Message = reader.Value.ToString()
        //                    }, statusCode);
        //                }
        //            }
        //        }
        //    }
        //    return default;
        //}
    }
}

