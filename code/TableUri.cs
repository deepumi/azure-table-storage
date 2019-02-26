namespace Azure.TableStorage
{
    internal sealed class TableUri
    {
        internal string Filter { get; } = string.Empty;

        internal string Uri { get; }

        internal TableUri(TableEntity entity, TableOperationType tableOperation)
        {
            Uri = tableOperation == TableOperationType.Insert ? 
                                    entity.TableName : 
                                    $"{entity.TableName}(PartitionKey='{entity.PartitionKey}',RowKey='{entity.RowKey}')";

            if (tableOperation == TableOperationType.Get && !string.IsNullOrEmpty(entity.SelectedProperties))
            {
                Filter = "?$select=" + entity.SelectedProperties;
            }
        }
    }
}
