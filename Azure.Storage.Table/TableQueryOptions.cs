namespace Azure.Storage.Table
{
    public sealed class TableQueryOptions
    {
        public int Top { get; set; }

        public string SelectProperties { get; set; }

        public FilterCondition[] Filters { get; set; }
    }
}