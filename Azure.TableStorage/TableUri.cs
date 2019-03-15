namespace Azure.TableStorage
{
    internal sealed class TableUri
    {
        internal string Filter { get; }

        internal string Url { get; }

        internal TableUri(ITableEntity entity, TableOperationType operationType, string filter)
        {
            if (operationType != TableOperationType.Insert && !string.IsNullOrEmpty(entity.PartitionKey) && !string.IsNullOrEmpty(entity.RowKey))
            {
                Url = $"{entity.TableName}(PartitionKey='{entity.PartitionKey}',RowKey='{entity.RowKey}')";
            }
            else
            {
                Url = entity.TableName;//+ "()";
            }

            Filter = filter;
        }
    }
}