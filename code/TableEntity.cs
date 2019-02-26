namespace Azure.TableStorage
{
    public abstract class TableEntity
    {
        public abstract string PartitionKey { get; set; }

        public abstract string RowKey { get; set; }

        internal abstract string TableName { get; }

        internal virtual string SelectedProperties { get; set; }
    }
}
