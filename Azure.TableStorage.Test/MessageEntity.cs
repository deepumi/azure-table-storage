namespace Azure.TableStorage.Test
{
    public sealed class MessageEntity : TableEntity
    {
        public string Message { get; set; }

        public MessageEntity() : base("DemoTable") { }
    }
}
