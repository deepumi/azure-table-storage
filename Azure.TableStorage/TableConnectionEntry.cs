namespace Azure.TableStorage
{
    internal sealed class TableConnectionEntry
    {
        internal string EndpointSuffix { get; set; }

        internal string AccountName { get; set; }

        internal string AccountKey { get; set; }
    }
}
