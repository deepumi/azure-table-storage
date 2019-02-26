namespace Azure.TableStorage.Test
{
    public sealed class MessageEntity : TableEntity
    {
        public override string PartitionKey { get; set; }

        public override string RowKey { get; set; }

        internal override string TableName { get; } = "Messages";

        //public override TableResult<T> DeSerialize<T>(Stream stream, HttpStatusCode statusCode)
        //{
        //    return new TableResult<T>(new MessageEntity(), HttpStatusCode.Accepted);
        //}
    }
}
